<script>
import mainSlider1 from "@/assets/law_firm/images/main-slider/1.jpg"
import mainSlider2 from "@/assets/law_firm/images/main-slider/2.jpg"
import background1 from "@/assets/law_firm/images/background/1.jpg"
import resourceService1 from "@/assets/law_firm/images/resource/service-1.jpg"

export default {
  name: "LowFirmPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Website for a small immigration law firm (solo practice).",
    }
  },
  computed: {},
  watch: {},
  methods: {
    bgImageURL(imageName) {
      const images = {
        mainSlider1: mainSlider1,
        mainSlider2: mainSlider2,
        background1: background1,
        resourceService1: resourceService1,
      }

      return {
        backgroundImage: `url(${images[imageName]})`,
      }
    },
  },
}
</script>

<template>
  <div class="page-wrapper">
    <!-- Main Header-->
    <header class="main-header">
      <!-- Header Top -->
      <div class="header-top">
        <div class="auto-container">
          <div class="inner-container clearfix">
            <!-- Top Left -->
            <div class="top-left pull-left">
              <div class="text">123 Sample Str, Welshpool, United Kingdom</div>
            </div>

            <!-- Top Right -->
            <div class="top-right pull-right">
              <!-- Info List -->
              <ul class="info-list">
                <li><a href="mailto:contact@lawa.com"> contact@lawa.com</a></li>
                <li><a href="tel:+003-2222-22-22-22">+003 1111 22 33 44</a></li>
              </ul>
            </div>
          </div>
        </div>
      </div>

      <!-- Header Upper -->
      <div class="header-upper">
        <div class="auto-container">
          <div class="clearfix">
            <div class="pull-left logo-box">
              <div class="logo">
                <a href="index.html"
                  ><img src="@/assets/law_firm/images/logo.png" alt="" title=""
                /></a>
              </div>
              <div class="logo-two">
                <a href="index.html"
                  ><img
                    src="@/assets/law_firm/images/logo-2.png"
                    alt=""
                    title=""
                /></a>
              </div>
            </div>

            <div class="nav-outer clearfix">
              <!-- Mobile Navigation Toggler -->
              <div class="mobile-nav-toggler">
                <span class="icon flaticon-menu"></span>
              </div>
              <!-- Main Menu -->
              <nav class="main-menu navbar-expand-md">
                <div class="navbar-header">
                  <button
                    class="navbar-toggler"
                    type="button"
                    data-toggle="collapse"
                    data-target="#navbarSupportedContent"
                    aria-controls="navbarSupportedContent"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                  >
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </button>
                </div>

                <div
                  class="navbar-collapse show collapse clearfix"
                  id="navbarSupportedContent"
                >
                  <ul class="navigation left-nav clearfix">
                    <li class="current"><a href="index.html">Home</a></li>
                    <li class="dropdown">
                      <a href="#">Service</a>
                      <ul>
                        <li><a href="about.html">About Us</a></li>
                        <li><a href="service.html">Service</a></li>
                        <li>
                          <a href="service-detail.html">Service Detail</a>
                        </li>
                      </ul>
                    </li>
                  </ul>

                  <ul class="navigation right-nav clearfix">
                    <li class="dropdown">
                      <a href="#">News</a>
                      <ul>
                        <li><a href="blog.html">Our Blog</a></li>
                        <li><a href="blog-detail.html">Blog Detail</a></li>
                      </ul>
                    </li>
                    <li><a href="contact.html">Contact</a></li>
                  </ul>
                </div>
              </nav>

              <!-- Social Box -->
              <ul class="social-box">
                <li>
                  <a href="https://twitter.com/"
                    ><span class="icofont-twitter"></span
                  ></a>
                </li>
                <li>
                  <a href="https://www.behance.net/"
                    ><span class="icofont-behance"></span
                  ></a>
                </li>
                <li>
                  <a href="https://www.instagram.com/"
                    ><span class="icofont-instagram"></span
                  ></a>
                </li>
              </ul>

              <!-- Outer Box -->
              <div class="outer-box">
                <ul class="language-nav">
                  <li><a href="#">Eng</a></li>
                  <li><a href="#">Fra</a></li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!--End Header Upper-->

      <!-- Mobile Menu  -->
      <div class="mobile-menu">
        <div class="menu-backdrop"></div>
        <div class="close-btn"><span class="icon lnr lnr-cross"></span></div>

        <nav class="menu-box">
          <div class="nav-logo">
            <a href="index.html"
              ><img src="@/assets/law_firm/images/logo-2.png" alt="" title=""
            /></a>
          </div>
          <div class="menu-outer">
            <!--Here Menu Will Come Automatically Via Javascript / Same Menu as in Header-->
          </div>
        </nav>
      </div>
      <!-- End Mobile Menu -->
    </header>
    <!--End Main Header -->

    <!-- Banner Section -->
    <section class="banner-section">
      <div class="main-slider-carousel swiper-container-horizontal">
        <div
          class="swiper-wrapper"
          style="
            transform: translate3d(-1665px, 0px, 0px);
            transition-duration: 0ms;
          "
        >
          <div
            class="swiper-slide swiper-slide-duplicate swiper-slide-prev"
            :style="bgImageURL('mainSlider1')"
            data-swiper-slide-index="2"
          >
            <div class="auto-container">
              <div class="content-boxed">
                <div class="inner-box">
                  <div class="title">lawa Law Firm</div>
                  <h1>
                    <span>Realiable</span> &amp; <span>Effective</span> <br />
                    Legal Solution
                  </h1>
                  <div class="text">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit,
                    <br />
                    sed diam nonummy nibh euismod tincidunt ut laoreet dolore
                    magna <br />
                    aliquam erat volutpat.
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div
            class="swiper-slide swiper-slide-active"
            :style="bgImageURL('mainSlider1')"
            data-swiper-slide-index="0"
          >
            <div class="auto-container">
              <div class="content-boxed">
                <div class="inner-box">
                  <div class="title">lawa Law Firm</div>
                  <h1>
                    <span>Realiable</span> &amp; <span>Effective</span> <br />
                    Legal Solution
                  </h1>
                  <div class="text">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit,
                    <br />
                    sed diam nonummy nibh euismod tincidunt ut laoreet dolore
                    magna <br />
                    aliquam erat volutpat.
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div
            class="swiper-slide swiper-slide-next"
            :style="bgImageURL('mainSlider1')"
            data-swiper-slide-index="1"
          >
            <div class="auto-container">
              <div class="content-boxed">
                <div class="inner-box">
                  <div class="title">lawa Law Firm</div>
                  <h1>
                    <span>Realiable</span> &amp; <span>Effective</span> <br />
                    Legal Solution
                  </h1>
                  <div class="text">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit,
                    <br />
                    sed diam nonummy nibh euismod tincidunt ut laoreet dolore
                    magna <br />
                    aliquam erat volutpat.
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div
            class="swiper-slide"
            :style="bgImageURL('mainSlider1')"
            data-swiper-slide-index="2"
          >
            <div class="auto-container">
              <div class="content-boxed">
                <div class="inner-box">
                  <div class="title">lawa Law Firm</div>
                  <h1>
                    <span>Realiable</span> &amp; <span>Effective</span> <br />
                    Legal Solution
                  </h1>
                  <div class="text">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit,
                    <br />
                    sed diam nonummy nibh euismod tincidunt ut laoreet dolore
                    magna <br />
                    aliquam erat volutpat.
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div
            class="swiper-slide swiper-slide-duplicate"
            :style="bgImageURL('mainSlider1')"
            data-swiper-slide-index="0"
          >
            <div class="auto-container">
              <div class="content-boxed">
                <div class="inner-box">
                  <div class="title">lawa Law Firm</div>
                  <h1>
                    <span>Realiable</span> &amp; <span>Effective</span> <br />
                    Legal Solution
                  </h1>
                  <div class="text">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit,
                    <br />
                    sed diam nonummy nibh euismod tincidunt ut laoreet dolore
                    magna <br />
                    aliquam erat volutpat.
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Slider Icon Scroll -->
      <div
        class="slider-icon-scroll scroll-to-target"
        data-target=".about-section"
      >
        <span class="icofont-scroll-down"></span>
      </div>
    </section>
    <!-- End Banner Section -->

    <!-- About Section -->
    <section class="about-section">
      <div class="auto-container">
        <!-- Upper Section -->
        <div class="upper-section">
          <div class="row clearfix">
            <!-- About Block -->
            <div class="about-block col-lg-4 col-md-6 col-sm-12">
              <div
                class="inner-box wow fadeInLeft"
                data-wow-delay="0ms"
                data-wow-duration="1500ms"
              >
                <div class="number">1</div>
                <div class="icon flaticon-checked"></div>
                <h3>
                  <a href="service-detail.html"
                    >Professional <br />
                    Liability</a
                  >
                </h3>
                <div class="text">
                  Lorem ipsum dolor sit amet, <br />
                  ctetur adipiscing elit. dapibus conse Tincidunt mollis
                  pretium.
                </div>
              </div>
            </div>

            <!-- About Block -->
            <div class="about-block col-lg-4 col-md-6 col-sm-12">
              <div
                class="inner-box wow fadeInUp"
                data-wow-delay="0ms"
                data-wow-duration="1500ms"
              >
                <div class="number">2</div>
                <div class="icon flaticon-checked"></div>
                <h3>
                  <a href="service-detail.html"
                    >Dedication To <br />
                    Our Clients</a
                  >
                </h3>
                <div class="text">
                  Lorem ipsum dolor sit amet, <br />
                  ctetur adipiscing elit. dapibus conse Tincidunt mollis
                  pretium.
                </div>
              </div>
            </div>

            <!-- About Block -->
            <div class="about-block col-lg-4 col-md-6 col-sm-12">
              <div
                class="inner-box wow fadeInRight"
                data-wow-delay="0ms"
                data-wow-duration="1500ms"
              >
                <div class="number">3</div>
                <div class="icon flaticon-checked"></div>
                <h3>
                  <a href="service-detail.html"
                    >Outstanding <br />
                    Services</a
                  >
                </h3>
                <div class="text">
                  Lorem ipsum dolor sit amet, <br />
                  ctetur adipiscing elit. dapibus conse Tincidunt mollis
                  pretium.
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Lower Section -->
        <div class="lower-section">
          <div class="row clearfix">
            <!-- Content Column -->
            <div class="content-column col-lg-6 col-sm-12 col-sm-12">
              <div class="inner-column">
                <div class="sec-title">
                  <div class="title">about us</div>
                  <h2>
                    The more law, the <br />
                    less Justice
                  </h2>
                </div>
                <div class="text">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  dapibus <br />
                  Tincidunt mollis pretium. Vivamus sodales eu sapien sitamet
                  iaculis. <br />
                  Curabitur mollis quam dolor, quis gravida orci mattis non. Nam
                  erat erat, <br />
                  tristique a elit at consectetur.
                </div>
                <h6>Mohamed Hana</h6>
                <div class="designation">CEO/Attorney of LAWa</div>
                <a href="about.html" class="theme-btn btn-style-one"
                  ><span class="txt">view more</span></a
                >
              </div>
            </div>

            <!-- Image Column -->
            <div class="image-column col-lg-6 col-sm-12 col-sm-12">
              <div
                class="inner-column wow slideInRight"
                data-wow-delay="0ms"
                data-wow-duration="1500ms"
              >
                <div class="image">
                  <img
                    src="@/assets/law_firm/images/resource/about.png"
                    alt=""
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- End Lower Section -->
      </div>
    </section>
    <!-- End About Section -->

    <!-- Services Section -->
    <section class="services-section">
      <div class="auto-container">
        <!-- Sec Title -->
        <div class="sec-title centered">
          <div class="title">pratices area</div>
          <h2>How we can’ help you</h2>
        </div>
        <div class="row clearfix">
          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-money-1"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Bank And <br />
                  Finacial</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-sedan-car-front"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Car & Motorcycle <br />
                  Accidents</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-shelter"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Personal <br />
                  Injury</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-dollar"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Family <br />
                  Law</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-trend"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Capital <br />
                  Markets</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-plumber"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Employment <br />
                  Law</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-building"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >House & Estate <br />
                  Litigation</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>

          <!-- Service Block -->
          <div class="service-block col-lg-3 col-md-6 col-sm-12">
            <div class="inner-box">
              <div
                class="image-layer"
                :style="bgImageURL('resourceService1')"
              ></div>
              <div class="icon-box">
                <span class="icon flaticon-handcuff"></span>
              </div>
              <h5>
                <a href="service-detail.html"
                  >Criminal <br />
                  Law</a
                >
              </h5>
              <div class="text">
                Lorem ipsum dolor sitori dolo <br />
                tur adipiscing elit.Aenean me <br />
                vel suscipit magna
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- End Services Section -->

    <!-- Testimonial Section -->
    <section class="testimonial-section">
      <div class="auto-container">
        <!-- Sec Title -->
        <div class="sec-title centered">
          <div class="title">testimonial</div>
          <h2>2,500+ Happy clients</h2>
        </div>
        <div class="testimonial-carousel">
          <div class="swiper-wrapper">
            <div class="swiper-slide">
              <!-- Testimonial Block -->
              <div class="testimonial-block">
                <div class="inner-box">
                  <div class="content">
                    <div class="author-image">
                      <img
                        src="@/assets/law_firm/images/resource/author-1.jpg"
                        alt=""
                      />
                    </div>
                    <div class="quote-icon icon_quotations"></div>
                    <div class="author">
                      Mohamed Hana <span>/ CEO at Google INC</span>
                    </div>
                    <div class="text">
                      Sed elit quam, iaculis sed semper sit amet udin vitae
                      nibh. <br />
                      at magna akal semperFusce commodo molestie luctus.Lorem
                      ipsum <br />
                      Dolor tusima olatiup.
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="swiper-slide">
              <!-- Testimonial Block -->
              <div class="testimonial-block">
                <div class="inner-box">
                  <div class="content">
                    <div class="author-image">
                      <img
                        src="@/assets/law_firm/images/resource/author-2.jpg"
                        alt=""
                      />
                    </div>
                    <div class="quote-icon icon_quotations"></div>
                    <div class="author">
                      Anabella Kleva <span>/ Managerment at Envato</span>
                    </div>
                    <div class="text">
                      Sed elit quam, iaculis sed semper sit amet udin vitae
                      nibh. <br />
                      at magna akal semperFusce commodo molestie luctus.Lorem
                      ipsum <br />
                      Dolor tusima olatiup aculis sed semper sit ame
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="swiper-slide">
              <!-- Testimonial Block -->
              <div class="testimonial-block">
                <div class="inner-box">
                  <div class="content">
                    <div class="author-image">
                      <img
                        src="@/assets/law_firm/images/resource/author-1.jpg"
                        alt=""
                      />
                    </div>
                    <div class="quote-icon icon_quotations"></div>
                    <div class="author">
                      Mohamed Hana <span>/ CEO at Google INC</span>
                    </div>
                    <div class="text">
                      Sed elit quam, iaculis sed semper sit amet udin vitae
                      nibh. <br />
                      at magna akal semperFusce commodo molestie luctus.Lorem
                      ipsum <br />
                      Dolor tusima olatiup.
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- Add Pagination -->
          <div class="swiper-pagination"></div>
        </div>
      </div>
    </section>
    <!-- End Testimonial Section -->

    <!-- Clients Section -->
    <section class="clients-section">
      <div class="auto-container">
        <div class="sponsors-outer">
          <!--Sponsors Carousel-->
          <div class="sponsors-carousel">
            <div class="swiper-wrapper">
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/1.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/2.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/3.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/4.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/5.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/1.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/2.png" alt=""
                  /></a>
                </figure>
              </div>
              <div class="swiper-slide">
                <figure class="image-box">
                  <a href="about.html"
                    ><img src="@/assets/law_firm/images/clients/3.png" alt=""
                  /></a>
                </figure>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- End Clients Section -->

    <!-- Team Section -->
    <section class="team-section">
      <div class="auto-container">
        <!-- Sec Title -->
        <div class="sec-title light centered">
          <div class="title">meet our attorneys</div>
          <h2>Dedicated & Devoted</h2>
        </div>
        <div class="team-carousel">
          <div class="swiper-wrapper">
            <div class="swiper-slide">
              <!-- Team Block -->
              <div class="team-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="service-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/team-1.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="lower-content">
                    <h3><a href="service-detail.html">Kate Farry</a></h3>
                    <div class="text">
                      Lorem ipsum dolor sit amet, consectetur adipis
                    </div>
                    <!-- Social Box -->
                    <ul class="social-box">
                      <li>
                        <a href="https://twitter.com/"
                          ><span class="icofont-twitter"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.behance.net/"
                          ><span class="icofont-behance"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.instagram.com/"
                          ><span class="icofont-instagram"></span
                        ></a>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>

            <div class="swiper-slide">
              <!-- Team Block -->
              <div class="team-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="service-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/team-2.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="lower-content">
                    <h3><a href="service-detail.html">Kate Farry</a></h3>
                    <div class="text">
                      Lorem ipsum dolor sit amet, consectetur adipis
                    </div>
                    <!-- Social Box -->
                    <ul class="social-box">
                      <li>
                        <a href="https://twitter.com/"
                          ><span class="icofont-twitter"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.behance.net/"
                          ><span class="icofont-behance"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.instagram.com/"
                          ><span class="icofont-instagram"></span
                        ></a>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>

            <div class="swiper-slide">
              <!-- Team Block -->
              <div class="team-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="service-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/team-3.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="lower-content">
                    <h3><a href="service-detail.html">Kate Farry</a></h3>
                    <div class="text">
                      Lorem ipsum dolor sit amet, consectetur adipis
                    </div>
                    <!-- Social Box -->
                    <ul class="social-box">
                      <li>
                        <a href="https://twitter.com/"
                          ><span class="icofont-twitter"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.behance.net/"
                          ><span class="icofont-behance"></span
                        ></a>
                      </li>
                      <li>
                        <a href="https://www.instagram.com/"
                          ><span class="icofont-instagram"></span
                        ></a>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Add Arrows -->
          <div class="swiper-button-next"></div>
          <div class="swiper-button-prev"></div>
        </div>
      </div>
    </section>
    <!-- Team Section -->

    <!-- Default Section -->
    <section class="default-section">
      <div class="auto-container">
        <div class="row clearfix">
          <!-- News Column -->
          <div class="news-column col-lg-6 col-md-12 col-sm-12">
            <div class="inner-column">
              <!-- Sec Title -->
              <div class="sec-title">
                <div class="title">media</div>
                <h2>Latest news</h2>
              </div>

              <!-- News Block -->
              <div class="news-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="blog-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/news-1.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="post-date">January, 27th, 2025</div>
                  <h4>
                    <a href="blog-detail.html"
                      >Things That You Need About House <br />
                      & Estate Law Before Feb 2017</a
                    >
                  </h4>
                </div>
              </div>

              <!-- News Block -->
              <div class="news-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="blog-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/news-2.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="post-date">January, 27th, 2025</div>
                  <h4><a href="blog-detail.html">Analysis Finacial 2025</a></h4>
                </div>
              </div>

              <!-- News Block -->
              <div class="news-block">
                <div class="inner-box">
                  <div class="image">
                    <a href="blog-detail.html"
                      ><img
                        src="@/assets/law_firm/images/resource/news-3.jpg"
                        alt=""
                    /></a>
                  </div>
                  <div class="post-date">January, 27th, 2025</div>
                  <h4>
                    <a href="blog-detail.html"
                      >Consulting About Family <br />
                      Law 2017</a
                    >
                  </h4>
                </div>
              </div>

              <a href="blog.html" class="articles">View All Articles</a>
            </div>
          </div>

          <!-- Accordion Column -->
          <div class="accordion-column col-lg-6 col-md-12 col-sm-12">
            <div class="inner-column">
              <!-- Sec Title -->
              <div class="sec-title">
                <div class="title">faqs</div>
                <h2>Maybe help you</h2>
              </div>

              <ul class="accordion-box">
                <!--Block-->
                <li class="accordion block active-block">
                  <div class="acc-btn active">
                    Can I Get A Divorce Without A Lawyer?
                    <div class="icon flaticon-down-arrow-1"></div>
                    <div class="cross-icon flaticon-multiply"></div>
                  </div>
                  <div class="acc-content current">
                    <div class="content">
                      <div class="text">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        Curabitur consectetur elit lacinia ornare. In volutpat
                        rutrum molestie. Vivamus efficitur orci, ac gravida eros
                        bibendum non. Nullam auctor varius fer Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices.
                      </div>
                    </div>
                  </div>
                </li>

                <!--Block-->
                <li class="accordion block">
                  <div class="acc-btn">
                    I Have A Technical Problem Or Support Issue I Need <br />
                    Resolved, Who Do I Email?
                    <div class="icon flaticon-down-arrow-1"></div>
                    <div class="cross-icon flaticon-multiply"></div>
                  </div>
                  <div class="acc-content">
                    <div class="content">
                      <div class="text">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        Curabitur consectetur elit lacinia ornare. In volutpat
                        rutrum molestie. Vivamus efficitur orci, ac gravida eros
                        bibendum non. Nullam auctor varius fer Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices.
                      </div>
                    </div>
                  </div>
                </li>

                <!--Block-->
                <li class="accordion block">
                  <div class="acc-btn">
                    What Other Services Are You Compatible With?
                    <div class="icon flaticon-down-arrow-1"></div>
                    <div class="cross-icon flaticon-multiply"></div>
                  </div>
                  <div class="acc-content">
                    <div class="content">
                      <div class="text">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        Curabitur consectetur elit lacinia ornare. In volutpat
                        rutrum molestie. Vivamus efficitur orci, ac gravida eros
                        bibendum non. Nullam auctor varius fer Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices.
                      </div>
                    </div>
                  </div>
                </li>

                <!--Block-->
                <li class="accordion block">
                  <div class="acc-btn">
                    Are You Hiring?
                    <div class="icon flaticon-down-arrow-1"></div>
                    <div class="cross-icon flaticon-multiply"></div>
                  </div>
                  <div class="acc-content">
                    <div class="content">
                      <div class="text">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        Curabitur consectetur elit lacinia ornare. In volutpat
                        rutrum molestie. Vivamus efficitur orci, ac gravida eros
                        bibendum non. Nullam auctor varius fer Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices.
                      </div>
                    </div>
                  </div>
                </li>
              </ul>

              <a href="about.html" class="questions">View All Questions</a>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- End Default Section -->

    <!-- Consulation Section -->
    <section class="consulation-section">
      <div class="auto-container">
        <div class="sec-title centered">
          <div class="title">get a quotes</div>
          <h2>Request a free consulation</h2>
        </div>
        <div class="inner-container">
          <!-- Consulation Form -->
          <div class="consulation-form">
            <!-- Consulation Form -->
            <form method="post" action="sendemail.php" id="contact-form">
              <div class="row clearfix">
                <div class="col-lg-4 col-md-6 col-sm-12 form-group">
                  <select name="country" class="custom-select-box">
                    <option>Select pratice areas</option>
                    <option>Areas 01</option>
                    <option>Areas 02</option>
                    <option>Areas 03</option>
                    <option>Areas 04</option>
                  </select>
                </div>

                <div class="col-lg-4 col-md-6 col-sm-12 form-group">
                  <input
                    type="text"
                    name="name"
                    placeholder="Your Name"
                    required
                  />
                </div>

                <div class="col-lg-4 col-md-12 col-sm-12 form-group">
                  <input
                    type="email"
                    name="email"
                    placeholder="Email Address"
                    required
                  />
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 form-group">
                  <textarea
                    name="message"
                    placeholder="Here goes your message"
                  ></textarea>
                </div>

                <div
                  class="col-lg-12 col-md-12 col-sm-12 text-center form-group"
                >
                  <button
                    class="theme-btn btn-style-one"
                    type="submit"
                    name="submit-form"
                  >
                    <span class="txt">send message</span>
                  </button>
                </div>
              </div>
            </form>
          </div>
          <!-- End Consulation Form -->
        </div>
      </div>
    </section>
    <!-- End Consulation Section -->

    <!-- CTA Section -->
    <section class="cta-section" :style="bgImageURL('background1')">
      <div class="auto-container">
        <div class="clearfix">
          <div class="pull-left">
            <div class="title">No Obligation. Free consultation</div>
            <a class="phone" href="tel:+0012-333-44-55">(0012) 333 44 55</a>
          </div>
          <div class="pull-right">
            <a href="contact.html" class="theme-btn btn-style-two"
              ><span class="txt">Contact Us</span></a
            >
          </div>
        </div>
      </div>
    </section>
    <!-- End CTA Section -->

    <!-- Main Footer -->
    <footer class="main-footer">
      <div class="auto-container">
        <div class="logo-box">
          <div class="logo">
            <a href="index.html"
              ><img src="@/assets/law_firm/images/logo.png" alt=""
            /></a>
          </div>
        </div>
        <ul class="footer-nav">
          <li><a href="about.html">Company</a></li>
          <li><a href="about.html">Services</a></li>
          <li><a href="about.html">Contact</a></li>
          <li><a href="about.html">Reviews & Media</a></li>
        </ul>
        <!-- Social Box -->
        <ul class="social-box">
          <li><a href="https://twitter.com/" class="icofont-twitter"></a></li>
          <li class="facebook">
            <a href="http://facebook.com/" class="icofont-facebook"></a>
          </li>
          <li class="google">
            <a href="https://www.google.com/" class="icofont-google-plus"></a>
          </li>
          <li class="behance">
            <a href="https://www.behance.net/" class="icofont-behance"></a>
          </li>
          <li class="instagram">
            <a href="https://www.instagram.com/" class="icofont-instagram"></a>
          </li>
        </ul>
        <div class="copyright">
          <p>&copy; 2024 <b class="text-white">LAWA</b></p>
        </div>
      </div>
    </footer>
    <!-- End Main Footer -->
  </div>
  <!--End pagewrapper-->

  <!-- Scroll To Top -->
  <div class="scroll-to-top scroll-to-target" data-target="html">
    <span class="fa fa-arrow-circle-up"></span>
  </div>
</template>

<style>
@import "@/assets/law_firm/css/main.css";
@import "@/assets/law_firm/css/responsive.css";
@import "styles.css";
</style>
