/**
 * @license
 * Copyright (C) 2010 The Libphonenumber Authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/**
 * @fileoverview  Phone Number Parser Demo.
 *
 * @author Nikolaos Trogkanis
 */

goog.require('goog.dom');
goog.require('goog.json');
goog.require('goog.proto2.ObjectSerializer');
goog.require('goog.string.StringBuffer');
goog.require('i18n.phonenumbers.AsYouTypeFormatter');
goog.require('i18n.phonenumbers.PhoneNumberFormat');
goog.require('i18n.phonenumbers.PhoneNumberType');
goog.require('i18n.phonenumbers.PhoneNumberUtil');
goog.require('i18n.phonenumbers.PhoneNumberUtil.ValidationResult');

function phoneNumberParser(phoneNumber, regionCode)
{
    var phoneUtil = i18n.phonenumbers.PhoneNumberUtil.getInstance();
    
    var PNV = i18n.phonenumbers.PhoneNumberUtil.ValidationResult;
    var PNE = i18n.phonenumbers.Error;

    var isValid = false;
    var errorCode = '';
    var type = '';
    var formatted = {};

    try {
        var number = phoneUtil.parseAndKeepRawInput(phoneNumber, regionCode);
        var isPossible = phoneUtil.isPossibleNumber(number);
        if (!isPossible) {
            switch (phoneUtil.isPossibleNumberWithReason(number)) {
                case PNV.INVALID_COUNTRY_CODE:
                    errorCode = PNE.INVALID_COUNTRY_CODE;
                    break;
                case PNV.TOO_SHORT:
                    errorCode = PNE.TOO_SHORT_NSN;
                    break;
                case PNV.TOO_LONG:
                    errorCode = PNE.TOO_LONG;
                    break;
            }

            isValid = false;
        }
        else {
            var isNumberValid = phoneUtil.isValidNumber(number);
            if (isNumberValid && regionCode && regionCode != 'ZZ') {
                isValid = phoneUtil.isValidNumberForRegion(number, regionCode);
                if (!isValid) {
                    errorCode = "Country Code not match with input country.";
                }
                else {
                    var PNT = i18n.phonenumbers.PhoneNumberType;
                    switch (phoneUtil.getNumberType(number)) {
                        case PNT.FIXED_LINE:
                            type = 'FIXED_LINE';
                            break;
                        case PNT.MOBILE:
                            type = 'MOBILE';
                            break;
                        case PNT.FIXED_LINE_OR_MOBILE:
                            type = 'FIXED_LINE_OR_MOBILE';
                            break;
                        case PNT.TOLL_FREE:
                            type = 'TOLL_FREE';
                            break;
                        case PNT.PREMIUM_RATE:
                            type = 'PREMIUM_RATE';
                            break;
                        case PNT.SHARED_COST:
                            type = 'SHARED_COST';
                            break;
                        case PNT.VOIP:
                            type = 'VOIP';
                            break;
                        case PNT.PERSONAL_NUMBER:
                            type = 'PERSONAL_NUMBER';
                            break;
                        case PNT.PAGER:
                            type = 'PAGER';
                            break;
                        case PNT.UAN:
                            type = 'UAN';
                            break;
                        case PNT.UNKNOWN:
                            type = 'UNKNOWN';
                            break;
                    }
                    var PNF = i18n.phonenumbers.PhoneNumberFormat;

                    formatted.e164 = phoneUtil.format(number, PNF.E164);
                    formatted.international = phoneUtil.format(number, PNF.INTERNATIONAL);
                    formatted.rfc3966 = phoneUtil.format(number, PNF.RFC3966);

                    goog.exportProperty(formatted, 'e164', formatted.e164);
                    goog.exportProperty(formatted, 'international', formatted.international);
                    goog.exportProperty(formatted, 'rfc3966', formatted.rfc3966);
                }
            }
            else {
                errorCode = PNE.NOT_A_NUMBER;
            }
        }
    }
    catch (e)
    {
        errorCode = e;
        isValid = false;
    }

    var result = { valid: isValid, formatted: formatted, type: type, code: errorCode };
    goog.exportProperty(result, 'valid', result.valid);
    goog.exportProperty(result, 'formatted', result.formatted);
    goog.exportProperty(result, 'type', result.type);
    goog.exportProperty(result, 'code', result.code);
    return result;
}

goog.exportSymbol('phoneNumberParser', phoneNumberParser);
