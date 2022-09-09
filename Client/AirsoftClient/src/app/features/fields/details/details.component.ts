import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DealerIdObj } from 'src/app/models/dealers/dealerIdObj';
import { FieldDetailsModel } from 'src/app/models/fields/fieldDetailsModel';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  isLoaded: boolean = false;

  slideIndex: number = 1;
  scrollTop: number = 0;
  @ViewChild('slides')
  slides!: ElementRef;

  field: FieldDetailsModel | undefined;
  isOwner: boolean = false;
  dealerObj: DealerIdObj | undefined;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isOwner = !this.authService.getClient() &&
      this.authService.isAuthenticated() &&
      this.dealerObj?.id == this.field?.dealerId;
  }

  plusSlides(n: any) {
    this.showSlides(this.slideIndex += n);
  }

  // Thumbnail image controls
  currentSlide(n: any) {
    this.showSlides(this.slideIndex = n);
  }

  showSlides(n: any) {
    let slidesElement: HTMLElement = this.slides.nativeElement;

    if (slidesElement.children.length > 0) {
      if (n >= slidesElement.children.length) {
        this.slideIndex = 0;
      }

      if (n < 1) {
        this.slideIndex = slidesElement.children.length;
      }

      if (this.slideIndex - 1 < 0) {
        this.slideIndex = 0;
      }
    }
  }
}
