import React from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Organizations from "../../Data/Data";
import styles from "./MostPopularOrganization.module.css"
import SliderCard from "../Organization/OrganizationsCard/SliderCard";
import NextArrow from "./Arrows/NextArrow";
import PrevArrow from "./Arrows/PrevArrow";


export default function MostPopularOrganization() {
  const settings = {
    className: "center",
    centerMode: true,
    infinite: true,
    centerPadding: "10px",
    slidesToShow: 4,
    speed: 500,
    dots: true,
    nextArrow: <NextArrow/>,
    prevArrow: <PrevArrow/>
  };

  return (
    <div className={styles.OrganizationsSlider}>
      <p>Our Recomended Organizations</p>
    <div className={styles.slaiderbox}>
      <Slider {...settings}>
        {Organizations.map((o) => (
        <SliderCard logoUrl={o.logoUrl} name={o.name} adress={o.adresses[0]}/>
      ))}
      </Slider>
      </div>
      </div>
  );
}