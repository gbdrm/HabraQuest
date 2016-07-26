import * as ng from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@ng.Component({
  selector: 'home',
  template: require('./home.html'),
  directives: [...ROUTER_DIRECTIVES]
})
export class Home {
    constructor() {
    }
}