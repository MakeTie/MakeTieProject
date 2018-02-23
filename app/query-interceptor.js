export class QueryInterceptor {

    constructor() {
        this.inputId = "lst-ib";
    }

    getQuery() {
        let script = `document.getElementById('${this.inputId}').value`;

        var promise = new Promise((resolve, reject) => {
            this.getCurrentTab((tab) => {
                chrome.tabs.executeScript(tab.id, {
                    code: script
                }, (query) => resolve(query));
            });
        });

        return promise;
    }

    getCurrentTab(callback) {
        var queryInfo = {
            active: true,
            currentWindow: true
        };

        chrome.tabs.query(queryInfo, (tabs) => {
            var tab = tabs[0];
            callback(tab);
        });
    }
}