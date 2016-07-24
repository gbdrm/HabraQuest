import { RouterConfig } from '@angular/router';
import { Home } from './components/home/home';
import { Results } from './components/results/results';
import { Quest } from './components/quest/quest';

export const routes: RouterConfig = [
    { path: '', redirectTo: 'home' },
    { path: 'home', component: Home },
    { path: 'quest', component: Quest },
    { path: 'results', component: Results },
    { path: '**', redirectTo: 'home' }
];
