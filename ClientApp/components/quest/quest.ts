import * as ng from '@angular/core';

@ng.Component({
    selector: 'quest',
    template: require('./quest.html')
})
export class Quest {
    public currentTask = 0;

    public nextTask() {
        // call API
    }
}
