export function beforeWebAssemblyStart(options, extensions) {
    var customScript = document.createElement('script');

    customScript.setAttribute('src', './communication-react.js');
    customScript.setAttribute('type', 'module');
    document.head.appendChild(customScript);
}