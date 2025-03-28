import { Component } from '@angular/core';

import { FormsModule } from '@angular/forms';





@Component({
    selector: 'app-request-shipping',
    imports: [ FormsModule],
    template: `
    <h1>CC</h1>
    `,
    styles: ``
})
export class RequestShippingComponent {
    calendarValue: any;
}
