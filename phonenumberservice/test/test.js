'use strict';

var assert = require('chai').assert;
var hapi = require('hapi');
var server = require('../server.js');

describe('plugin loading', function ()
{
    describe('inert plugin', function () {
        it('should have no err when load plugin', function (done) {
            var hapiServer = new hapi.Server();
            hapiServer.register(require('inert'), function (err) {
                assert.equal(err, null, 'should have null err');
                done();
            });
        });
    });
});

describe('server config', function () {
    describe('route table', function ()
    {
        it('should have two routes', function () {
            var table = server.table()[0].table;
            assert.lengthOf(table, 2, 'route table have 2 items');
            assert.equal(table[0].path, '/{p*}', 'route for all catch');
            assert.equal(table[1].path, '/phonenumber/{number}', 'route for phonenumber API');
            assert.equal(table[1].method, 'get', 'method GET for phonenumber API');
        });
    });
});

describe('server help page', function () {
    describe('request random path', function () {
        it('success for a help page', function (done) {
            server.inject({ method: 'GET', url: '/randompage' }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                assert.equal(res.headers['content-type'], 'text/html; charset=utf-8', 'A help html page returned');
                assert.isAtLeast(res.payload.indexOf('Help Page'), 0, 'Html page contain Help Page title.');
            });

            done();
        });
    });
});

// TODO:
// one big problem here: the exception in server.jnject will not throw out as it has been caugth by domain
// this will make the assert fail still be seen as success test
// searched online and there are a bunch of doc about this, will address later
describe('service API - /phonenumber', function ()
{
    describe('request with country param', function ()
    {
        it('success with valid phone number and corresponding country', function (done)
        {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106?country=nz' }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
        it('fail with valid phone number and unmatched country', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/%2b64212566106?country=uk' }, function (res) {
                assert.equal(res.statusCode, 400, 'fail statusCode 400 return');
                var result = JSON.parse(res.payload);
                assert.isNotTrue(result.success, 'Success is false for failed call');
                assert.equal(result.code, 'Country Code not match with input country.', 'Error code returned');
                assert.equal(result.message, 'Country Code not match with input country.', 'Error message returned');
            });
            done();
        });
        it('fail with valid phone number and invalid country', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/012566106?country=zz' }, function (res) {
                assert.equal(res.statusCode, 400, 'fail statusCode 400 return');
                var result = JSON.parse(res.payload);
                assert.isNotTrue(result.success, 'Success is false for failed call');
                assert.equal(result.code, 'Invalid country calling code', 'Error code returned');
                assert.equal(result.message, 'Invalid country calling code', 'Error message returned');
            });
            done();
        });
        it('fail with invalid phone number - too short', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/%2b642125?country=nz' }, function (res) {
                assert.equal(res.statusCode, 400, 'fail statusCode 400 return');
                var result = JSON.parse(res.payload);
                assert.isNotTrue(result.success, 'Success is false for failed call');
                assert.equal(result.code, 'The string supplied is too short to be a phone number', 'Error code returned');
                assert.equal(result.message, 'The string supplied is too short to be a phone number', 'Error message returned');
            });
            done();
        });
    });
    
    describe('request without country param', function ()
    {
        it('success with valid phone number and corresponding country', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106', headers: { 'x-client-ip': '122.60.96.68' } }, function (res) {
                console.log(res.request.headers);
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
        it('fail with valid phone number and invalid country', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/012566106', headers: { 'x-client-ip': '127.0.0.1' } }, function (res) {
                assert.equal(res.statusCode, 400, 'fail statusCode 400 return');
                var result = JSON.parse(res.payload);
                assert.isNotTrue(result.success, 'Success is false for failed call');
                assert.equal(result.code, 'Invalid country calling code', 'Error code returned');
                assert.equal(result.message, 'Invalid country calling code', 'Error message returned');
            });
            done();
        });
    });

    describe('request without country param - test header params/remote address order', function () {
        it('success with valid x-client-ip and invalid x-forwarded-for', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106', headers: { 'x-client-ip': '122.60.96.68', 'x-forwarded-for': '127.0.0.1' } }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
        it('success with valid x-forwarded-for and invalid x-real-ip', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106', headers: { 'x-forwarded-for': '122.60.96.68', 'x-real-ip': '127.0.0.1' } }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
        it('success with valid x-real-ip and invalid remote address', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106', headers: { 'x-client-ip': '122.60.96.68' }, remoteAddress: '127.0.0.1' }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
        it('success with valid remote address', function (done) {
            server.inject({ method: 'GET', url: '/phonenumber/0212566106', remoteAddress: '122.60.96.68' }, function (res) {
                assert.equal(res.statusCode, 200, 'success statusCode 200 return');
                var result = JSON.parse(res.payload);
                assert.isTrue(result.valid, 'Valid is true for success call');
                assert.equal(result.type, 'MOBILE', 'Correct type "MOBILE" returned');
                assert.equal(result.formatted.e164, '+64212566106', 'Correct e164 format returned');
                assert.equal(result.formatted.international, '+64 21 256 6106', 'Correct international format returned');
                assert.equal(result.formatted.rfc3966, 'tel:+64-21-256-6106', 'Correct rfc3966 format returned');
            });
            done();
        });
    });
});