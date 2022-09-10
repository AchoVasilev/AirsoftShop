import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerIdObj } from 'src/app/models/dealers/dealerIdObj';
import { FieldDetailsModel } from 'src/app/models/fields/fieldDetailsModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { DataService } from 'src/app/services/data/data.service';
import { DealerService } from 'src/app/services/dealer/dealer.service';
import { FieldService } from 'src/app/services/fields/field.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  isLoaded: boolean = false;

  fieldId: number = this.route.snapshot.params['id'];

  scrollTop: number = 0;
  @ViewChild('slides')
  slides!: ElementRef;

  field!: FieldDetailsModel;
  isOwner: boolean = false;
  dealerObj: DealerIdObj | undefined;

  constructor(
    private authService: AuthService,
    private fieldService: FieldService,
    private dealerService: DealerService,
    private dataService: DataService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.dealerService.getDealerId()
      .subscribe(dealer => this.dealerObj = dealer);

    this.fieldService.detailsById(this.fieldId)
      .subscribe(data => {
        this.field = data;
        this.dataService.changeFieldModel(data);
      });

    this.isOwner = !this.authService.getClient() &&
      this.authService.isAuthenticated() &&
      this.dealerObj?.id == this.field?.dealerId;

    this.isLoaded = true;
    this.isLoading = false;
  }

  nextSlide() {
    let slidesElement: HTMLElement = this.slides.nativeElement;
    let slide = document.querySelector('.slide');
    slidesElement.scrollLeft += slide!.clientWidth;
    console.log(slidesElement.scrollLeft);
  }

  prevSlide() {
    let slidesElement: HTMLElement = this.slides.nativeElement;
    let slide = document.querySelector('.slide');
    slidesElement.scrollLeft -= slide!.clientWidth;
  }

  onDelete(id: number) {
    this.isLoading = true;
    this.isLoaded = false;

    this.fieldService.deleteById(id)
      .subscribe({
        next: () => {
          this.toastr.success("Успешно изтриване");
          this.isLoading = false;
          this.isLoaded = true;

          this.router.navigate(['/fields/mine']);
        }
      }
      )
  }
}
