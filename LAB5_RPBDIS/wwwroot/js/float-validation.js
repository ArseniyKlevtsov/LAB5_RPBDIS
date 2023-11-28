$.validator.addMethod("float", function (value, element) {
    value = value.replace(",", ".");
    return this.optional(element) || /^-?\d+(\.\d+)?$/.test(value);
}, "Please enter a valid float number.");