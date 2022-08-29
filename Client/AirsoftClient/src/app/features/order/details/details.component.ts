import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderDetailsModel } from 'src/app/models/orders/orderDetailsModel';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  details!: OrderDetailsModel;
  isLoaded: boolean = false;
  isLoading: boolean = true;
  currentDate = new Date().toLocaleDateString('bg-BG',);

  constructor(private orderService: OrderService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getOrderDetails();
    this.isLoaded = true;
    this.isLoading = false;
  }

  getOrderDetails(): void {
    const orderId = this.route.snapshot.params['id'];
    this.orderService.getOrderDetails(orderId)
      .subscribe(res => {
        this.details = res;
      });
  }
}
