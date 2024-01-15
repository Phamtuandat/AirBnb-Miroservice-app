export type Property = {
  medias: Media[];
  id: string;
  title: string;
  description: string;
  locationDetail: string;
  city: string;
  country: string;
  slug: string;
  pricePerNight: string;
  amenitie: string;
  hostId: string;
  typeId: string;
  numberOfBethroom: number;
  numberOfBedroom: number;
  maxGuests: number;
  averageRate: number;
  createAt: string;
};

export type Media = {
  id: '6d8f838a-3a95-4b72-bceb-58f49bceb302';
  url: 'https://loremflickr.com/640/480/house/any?lock=1015017298';
  isThumb: true;
  propertyId: '9e63728a-4dec-fadd-c5c1-bd6c73cf21e4';
};
