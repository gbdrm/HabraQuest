import * as ng from '@angular/core';
import {Inject} from '@angular/core';
import { Http } from '@angular/http';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@ng.Component({
    selector: 'quest',
    template: require('./quest.html')
})
export class Quest {
    private task: any = {};
    private answer: string = '';

    constructor( @Inject(Http) private http: Http) {
        this.http.get('/home/getcurrentstate', { withCredentials: true })
            .subscribe(result => {
                this.task = result.json().task;
                let token = Cookie.get('playerToken');
                if (!token) {
                    token = result.json().player.token;
                    Cookie.set('playerToken', token);
                }
            });
    }

    submit() {
        this.http.get('/home/submitanswer?answer=' + this.answer, { withCredentials: true })
            .subscribe(result => {
                this.task = result.json().task;
                this.answer = '';
            });
    }
}
