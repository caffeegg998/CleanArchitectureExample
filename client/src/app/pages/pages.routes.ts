import { Routes } from '@angular/router';
import { Documentation } from './documentation/documentation';
import { Crud } from './crud/crud';
import { Empty } from './empty/empty';
import { RequestShippingComponent } from './request-shipping/request-shipping.component';

export default [
    { path: 'documentation', component: Documentation },
    { path: 'request-shipping', component: RequestShippingComponent },
    { path: 'crud', component: Crud },
    { path: 'empty', component: Empty },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
