var slideIndex = 1;
showSlides(slideIndex);

var startIndex = 0;
autoShowSlides();

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName('field-image');

    if (n > slides.length) {
        slideIndex = 1;
    }

    if (n < 1) {
        slideIndex = slides.length;
    }

    for (i = 0; i < slides.length; i++) {
        // @ts-ignore
        slides[i].style.display = 'none';
    }

    // @ts-ignore
    slides[slideIndex - 1].style.display = 'block';
}

function autoShowSlides() {
    var i;
    var slides = document.getElementsByClassName('field-image');
    for (i = 0; i < slides.length; i++) {
        // @ts-ignore
        slides[i].style.display = 'none';
    }

    startIndex++;
    if (startIndex > slides.length) {
        startIndex = 1;
    }

    // @ts-ignore
    slides[startIndex - 1].style.display = 'block';

    setTimeout(autoShowSlides, 4000); // Change image every 2 seconds
}