var Validator = {
    materialType: {
        identifierMaxSize: 50,
        maxLengthValidation: function (input) {
            return Validator.maxLengthValidation(input, 'identifier', Validator.materialType.identifierMaxSize);
        }
    },
    maxLengthValidation: function (input, name, maxLength) {
        if (!input.is('[name="' + name + '"]')) {
            return true;
        }

        input.attr("data-maxLengthValidation-msg", i18n.Resources.ValidateMaxLengthMessage.replace('{2}', maxLength));

        return input.val().length <= maxLength;
    }
};