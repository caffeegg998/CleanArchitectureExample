import { Component } from '@angular/core';
import { ButtonDirective } from 'primeng/button';
import { CurrencyPipe } from '@angular/common';
import { Ripple } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { Product, ProductService } from '../../service/product.service';


@Component({
    selector: 'app-recent-product-widget',
    imports: [ButtonDirective, CurrencyPipe, Ripple, TableModule],
    template: `
        <div class="card !mb-8">
            <div class="font-semibold text-xl mb-4">Sản phẩm đang chạy</div>
            <p-table [value]="products" [paginator]="true" [rows]="5" responsiveLayout="scroll">
                <ng-template #header>
                    <tr>
                        <th>Image</th>
                        <th pSortableColumn="name">Name <p-sortIcon field="name"></p-sortIcon></th>
                        <th pSortableColumn="price">Price <p-sortIcon field="price"></p-sortIcon></th>
                        <th>View</th>
                    </tr>
                </ng-template>
                <ng-template #body let-product>
                    <tr>
                        <td style="width: 15%; min-width: 5rem;">
                            <img src="https://primefaces.org/cdn/primevue/images/product/{{ product.image }}" class="shadow-lg" alt="{{ product.name }}" width="50" />
                        </td>
                        <td style="width: 35%; min-width: 7rem;">{{ product.name }}</td>
                        <td style="width: 35%; min-width: 8rem;">{{ product.price | currency: 'USD' }}</td>
                        <td style="width: 15%;">
                            <button pButton pRipple type="button" icon="pi pi-search" class="p-button p-component p-button-text p-button-icon-only"></button>
                        </td>
                    </tr>
                </ng-template>
            </p-table>
        </div>
    `,

    providers: [ProductService],
    styles: ``
})
export class RecentProductWidgetComponent {
    products!: Product[];

    constructor(private productService: ProductService) {}

    ngOnInit() {
        this.productService.getProductsSmall().then((data) => (this.products = data));
    }
}
