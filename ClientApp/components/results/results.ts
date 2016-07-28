import * as ng from '@angular/core';
import { Http } from '@angular/http';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@ng.Component({
    selector: 'results',
    template: require('./results.html')
})
export class Results {
    private showres = false;
    private results: IResultItem[];

    constructor(http: Http) {
        this.showres = Cookie.get('q_finished') === 'yes';

        http.get('/api/Results/Fetch').subscribe(result => {
            this.results = result.json();
        });
    }
}

interface IResultItem {
    rank: number;
    dateFormatted: string;
    name: string;
    summary: string;
}
