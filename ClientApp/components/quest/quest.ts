import * as ng from '@angular/core';
import {Inject} from '@angular/core';
import { Http } from '@angular/http';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import {Router} from '@angular/router';

@ng.Component({
    selector: 'quest',
    template: require('./quest.html')
})
export class Quest {
    private task: any = {};
    private answer: string = '';

    private feedback = '';
    private hintColor = '#34bb34';

    private hasFinished = false;
    private finishForm;

    constructor( @Inject(Http) private http: Http, private router: Router) {
        const showres = Cookie.get('q_finished') === 'yes';
        if (showres) this.router.navigateByUrl('results');
        else
            this.http.get('/home/getcurrentstate', { withCredentials: true })
                .subscribe(result => {
                    const res = result.json();
                    this.hasFinished = res.player.hasFinished;
                    this.task = res.task ? res.task : {};
                    let token = Cookie.get('playerToken');
                    if (!token) {
                        token = res.player.token;
                        Cookie.set('playerToken', token);
                    }
                });
    }

    submit() {
        this.http.get('/home/submitanswer?answer=' + this.answer, { withCredentials: true })
            .subscribe(result => {
                const res = result.json();
                if (res.player.hasFinished) {
                    this.hasFinished = true;
                    this.answer = '';
                    this.task = {};
                    this.feedback = '';
                    return;
                }
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

    finish(form): void {
        this.http.post('/home/finish', form, { withCredentials: true })
            .subscribe(result => {
                if (result.json().statusCode == 200) {
                    Cookie.set('q_finished', 'yes');
                    this.router.navigateByUrl('results');
                }
                else if (this.hasFinished) {
                    Cookie.set('playerToken', '');
                    this.hasFinished = false;
                }
            });
    }
}
