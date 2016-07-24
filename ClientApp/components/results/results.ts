import * as ng from '@angular/core';
import { Http } from '@angular/http';

@ng.Component({
    selector: 'results',
    template: require('./results.html')
})
export class Results {
    public results: IResultItem[];

    constructor(http: Http) {
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
