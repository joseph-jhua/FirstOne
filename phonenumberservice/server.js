'use strict';

var hapi = require('hapi');
var http = require('http');

// the lib js compiled using google's phone number util and a small interface of my own
require('./libphonenumber-compiled');

var server = new hapi.Server();

server.connection({ port: 8888});

// register the static help page for all catch route
server.register(require('inert'), (err) => {
    if (err) {
        throw err;
	}

    // catch all route to default help page
	server.route({
	    method: '*',
	    path: '/{p*}',
	    handler: function (request, reply) {
	        reply.file('./public/help.html');
	    }
	});
});

// /phonenumber/{number} API route
server.route({
	method: 'GET',
	path: '/phonenumber/{number}',
	handler: function (request, reply) {

	    var generateReply = function (countryCode) {
	        var parseResult = phoneNumberParser(request.params.number, countryCode);
	        if (parseResult.valid) {
	            reply({ valid: parseResult.valid, formatted: { e164: parseResult.formatted.e164, international: parseResult.formatted.international, rfc3966: parseResult.formatted.rfc3966 }, type: parseResult.type })
                    .type('application/json');
	        }
	        else {
	            reply({ success: parseResult.valid, code: parseResult.code, message: parseResult.code })
                    .type('application/json')
                    .code(400);
	        }
	    };

        // ip address fetch order
	    var ipForResolve = request.headers["x-client-ip"] || request.headers["x-forwarded-for"] || request.headers["x-real-ip"] || request.info.remoteAddress;
	    var countryCode = "ZZ";
	    if (!('country' in request.query) && ipForResolve) {
            // use a external api to resolve ip address to country code
	        var ipResolveAPI = {
	            host: 'localizationapi.findly.com',
	            path: '/api/geoip/' + ipForResolve
	        };
	        var callback = function (response) {
	            var body = [];
	            response.on('data', function (chunk) {
	                body.push(chunk);
	            }).on('end', function () {
	                var apiResult = JSON.parse(Buffer.concat(body).toString());
	                if (apiResult && apiResult.isoAlpha2) {
	                    countryCode = apiResult.isoAlpha2;
	                }
	                generateReply(countryCode);
	            });
	        };

	        http.get(ipResolveAPI, callback);
	    }
	    else {
	        if ('country' in request.query)
	        {
	            countryCode = request.query.country;
	        }
	        generateReply(countryCode);
	    }
	}
});

if (!module.parent) {
    server.start((err) => {
        if (err) {
            throw err;
        }
        console.log('Server running at: ', server.info.uri);
    });
}

module.exports = server;