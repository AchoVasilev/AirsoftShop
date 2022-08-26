import { Component, OnInit } from '@angular/core';
import { DealerGunsList } from 'src/app/models/products/guns/dealerGunList';
import { ProductService } from 'src/app/services/products/product.service';

@Component({
  selector: 'app-mine',
  templateUrl: './mine.component.html',
  styleUrls: ['./mine.component.css']
})
export class MineComponent implements OnInit {
  guns: DealerGunsList[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadGuns();
    this.isLoading = false;
    this.isLoaded = true;
  }

  loadGuns(): void {
    this.productService.getDealerProducts()
      .subscribe(res => this.guns = res);
  }
}
