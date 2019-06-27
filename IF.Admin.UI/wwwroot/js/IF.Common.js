function getFunction(code, argNames) {
    var fn = window, parts = (code || "").split(".");
    while (fn && parts.length) {
        fn = fn[parts.shift()];
    }
    if (typeof (fn) === "function") {
        return fn;
    }
    argNames.push(code);
    return Function.constructor.apply(null, argNames);
}


function isdefined(variable) {
    return (typeof (window[variable]) === "undefined") ? false : true;
}