import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  slideIndex: number = 1;
  scrollTop: number = 0;
  isLoaded: boolean = false;
  isLoading: boolean = true;

  @ViewChild('slides')
  slides!: ElementRef;

  constructor() { }

  ngOnInit(): void {
    this.isLoaded = true;
    this.isLoading = false;
  }

  ngAfterViewInit(): void {
    this.scroll();
    setInterval(() => this.autoShowSlides(), 10000);
    this.showSlides(this.slideIndex);
  }

  scroll() {
    document.addEventListener('scroll', (ev) => {
      if (document.documentElement.scrollTop > 450 && document.documentElement.scrollTop <= 3500) {
        this.scrollTop = document.documentElement.scrollTop;
      }

      // if (document.documentElement.scrollTop > 450) {
      //   this.router.navigate([{ outlets: { first: ['/home/first'] } }]);
      // }
    })
  }

  // Next/previous controls
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

  autoShowSlides() {
    let slidesElement: HTMLElement = this.slides.nativeElement;

    if (slidesElement.children.length > 0) {

      if (this.slideIndex == undefined) {
        this.slideIndex = 0;
      }

      this.slideIndex++;
      if (this.slideIndex >= slidesElement.children.length) {
        this.slideIndex = 0;
      }
    }
  }
}
