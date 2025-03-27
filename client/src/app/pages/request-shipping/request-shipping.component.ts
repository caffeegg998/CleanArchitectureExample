import { Component } from '@angular/core';
import { BestSellingWidget } from '../dashboard/components/bestsellingwidget';
import { NotificationsWidget } from '../dashboard/components/notificationswidget';
import { RevenueStreamWidget } from '../dashboard/components/revenuestreamwidget';
import { StatswidgetComponent } from './components/statswidget.component';
import { RecentProductWidgetComponent } from './components/recent-product-widget.component';
import { DatePicker, DatePickerModule } from 'primeng/datepicker';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';





@Component({
    selector: 'app-request-shipping',
    imports: [BestSellingWidget, NotificationsWidget, RevenueStreamWidget, StatswidgetComponent, RecentProductWidgetComponent, DatePickerModule, DatePicker, FormsModule],
    template: `
        <div class="grid grid-cols-12 gap-4">
            <div class="card flex justify-between items-center !p-5 !m-0 col-span-12">
                <p class="!m-0  text-2xl font-bold">Trang tổng quan</p>
                <div class="font-semibold text-xl">
                    Thống kê theo tháng:
                    <p-datepicker
                        class="w-12"
                        view="month"
                        dateFormat="mm/yy"
                        [showIcon]="true"
                        [showButtonBar]="true"
                        [(ngModel)]="calendarValue"
                    ></p-datepicker>
                </div>
            </div>
            <app-statswidget class="contents" />
            <div class="col-span-12 xl:col-span-6">
                <app-recent-product-widget />
                <app-best-selling-widget />
            </div>
            <div class="col-span-12 xl:col-span-6">
                <app-revenue-stream-widget />
                <app-notifications-widget />
            </div>
        </div>
    `,
    styles: ``
})
export class RequestShippingComponent {
    calendarValue: any;
}
