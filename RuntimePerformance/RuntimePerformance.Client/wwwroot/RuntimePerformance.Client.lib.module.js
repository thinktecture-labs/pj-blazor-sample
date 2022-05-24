export function beforeStart(options, extensions) {
    console.log("beforeStart", new Date().toISOString());
}

export function afterStarted(blazor) {
    console.log("afterStarted", new Date().toISOString());
}