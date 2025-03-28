import { Component } from '@angular/core';

@Component({
  selector: 'app-statswidget',
  imports: [],
  template: `
      <div class="col-span-12 lg:col-span-6 xl:col-span-3">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Pending</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">12 Đơn chờ xử lý</div>
                  </div>
                  <div class="flex items-center justify-center bg-blue-100 dark:bg-blue-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-shopping-cart text-blue-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">24 new </span>
              <span class="text-muted-color">since last visit</span>
          </div>
      </div>
      <div class="col-span-12 lg:col-span-6 xl:col-span-3">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Processed</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">152 Đã tiếp nhận</div>
                  </div>
                  <div class="flex items-center justify-center bg-orange-100 dark:bg-orange-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-dollar text-orange-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">%52+ </span>
              <span class="text-muted-color">since last week</span>
          </div>
      </div>
      <div class="col-span-12 lg:col-span-6 xl:col-span-3">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Shipping</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">24 chuyển</div>
                  </div>
                  <div class="flex items-center justify-center bg-cyan-100 dark:bg-cyan-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-users text-cyan-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">520 </span>
              <span class="text-muted-color">newly registered</span>
          </div>
      </div>
      <div class="col-span-12 lg:col-span-6 xl:col-span-3">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Delivered</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">152 Đã giao hàng</div>
                  </div>
                  <div class="flex items-center justify-center bg-purple-100 dark:bg-purple-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-comment text-purple-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">85 </span>
              <span class="text-muted-color">responded</span>
          </div>
      </div>
      <div class="col-span-12 lg:col-span-6 xl:col-span-6">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Cancelled</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">122 Đơn hủy</div>
                  </div>
                  <div class="flex items-center justify-center bg-purple-100 dark:bg-purple-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-comment text-purple-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">85 </span>
              <span class="text-muted-color">responded</span>
          </div>
      </div>
      <div class="col-span-12 lg:col-span-6 xl:col-span-6">
          <div class="card mb-0">
              <div class="flex justify-between mb-4">
                  <div>
                      <span class="block text-muted-color font-medium mb-4">Returned</span>
                      <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">152 Đơn hoàn</div>
                  </div>
                  <div class="flex items-center justify-center bg-purple-100 dark:bg-purple-400/10 rounded-border" style="width: 2.5rem; height: 2.5rem">
                      <i class="pi pi-comment text-purple-500 !text-xl"></i>
                  </div>
              </div>
              <span class="text-primary font-medium">85 </span>
              <span class="text-muted-color">responded</span>
          </div>
      </div>
  `,
  styles: ``
})
export class StatswidgetComponent {

}
