export function beforeWebAssemblyStart(options, extensions) {
    var customScript = document.createElement('script');

    customScript.setAttribute('src', './communication-react.js');
    document.head.appendChild(customScript);
}