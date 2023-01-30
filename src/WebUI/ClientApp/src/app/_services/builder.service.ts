import { Injectable } from '@angular/core';
import { ContactsClient, ContactType, CountriesClient,  CustomersClient, SectorsClient, TagsClient, TargetsWorkClient, TechnologiesClient, TribeOffersClient, VerticalsClient } from '../web-api-client';

@Injectable({
  providedIn: 'root'
})
export class BuilderService {

  constructor(private verticalsClient: VerticalsClient, private sectorsClient: SectorsClient, 
    private customersClient: CustomersClient, private countriesClient: CountriesClient,
    private targetsClient: TargetsWorkClient, private tagsClient: TagsClient, private offersClient: TribeOffersClient,
    private technologiesClient: TechnologiesClient, private contactsClient: ContactsClient) { }

  GetVerticals() {
    return this.verticalsClient.getVerticals();
  }

  GetSectorsByVertical(verticalId: number) {
    return this.sectorsClient.getSectorsByVertical(verticalId);
  }

  GetCustomersBySector(sectorId: number) {
    return this.customersClient.getCustomersBy(sectorId);
  }

  GetCountries() {
    return this.countriesClient.getCountries();
  }

  GetCountryById(customerId: number){
    return this.countriesClient.getCountry(customerId);
  }

  GetTargetsWork() {
    return this.targetsClient.getTargetsWorkDTO();
  }
  
  GetTags() {
    return this.tagsClient.getTags();
  }

  GetTribeOffers() {
    return this.offersClient.getTribeOffers();
  }

  GetTechnologies() {
    return this.technologiesClient.getTechnologies();
  }

  GetTechnologyById(technologyId: number) {
    return this.technologiesClient.getTechnology(technologyId);
  }

  GetContactsByType(type: ContactType) {
    return this.contactsClient.getContactsByType(type);
  }

  GetContactById(contactId: number) {
    return this.contactsClient.getContact(contactId);
  }

}
