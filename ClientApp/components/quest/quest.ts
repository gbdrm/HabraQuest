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

    private feedback = '';
    private hintColor = '#34bb34';

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
                const res = result.json();
                this.task = res.task;
                this.feedback = res.feedback;
                this.answer = '';
            });
        this.refreshHintStyle()
    }

    // set random color to hint
    refreshHintStyle() {
        var letters = '0123456789ABC'.split('');
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 13)];
        }
        this.hintColor = color;
    };
}
