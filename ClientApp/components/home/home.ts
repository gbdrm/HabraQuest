import * as ng from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import { Http } from '@angular/http';

@ng.Component({
  selector: 'home',
  template: require('./home.html'),
  directives: [...ROUTER_DIRECTIVES],
})
export class Home {
    constructor(private http: Http) { }

    ngOnInit() {
        let token = Cookie.get('playerToken');
        if (!token) {
            this.http.get('/home/registernewplayer').subscribe(result => {
                token = result.text();
                Cookie.set('playerToken', token);
            });
        }
    }
}
