import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { ContactsClient, ContactType, CountriesClient, ProjectContacts, ProjectCountries, ProjectDTO, ProjectsClient, ProjectTags, ProjectTargetsWork, ProjectTechnologies, ProjectTribeOffers, TagsClient, TargetsWorkClient, TechnologiesClient, TribeOffersClient } from '../web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ReferenceService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private projectsClient: ProjectsClient, private countriesClient: CountriesClient, 
    private targetsClient: TargetsWorkClient, private tagsClient: TagsClient, private offersClient: TribeOffersClient,
    private technologiesClient: TechnologiesClient, private contactsClient: ContactsClient) { }

  addReference(projectToCreate:ProjectDTO){
    return this.projectsClient.create(projectToCreate);
  }

  createLinkProjectCountry(projectId: number, countryId: number){
    let projectLink = new ProjectCountries({projectId, countryId});
    return this.countriesClient.createProjectCountryLink(projectLink);
  }

  createLinkProjectTechnology(projectId: number, technologyId: number){
    let projectLink = new ProjectTechnologies({projectId, technologyId});
    return this.technologiesClient.createProjectTechnologyLink(projectLink);
  }

  createLinkProjectContact(projectId: number, contactId: number, contactType: ContactType){
    let projectLink = new ProjectContacts({projectId, contactId, contactType});
    return this.contactsClient.createProjectContactLink(projectLink);
  }
  createLinkProjectTag(projectId: number, tagId: number){
    let projectLink = new ProjectTags({projectId, tagId});
    return this.tagsClient.createProjectTagLink(projectLink);
  }
  createLinkProjectTargetWork(projectId: number, targetWorkId: number){
    let projectLink = new ProjectTargetsWork({projectId, targetWorkId});
    return this.targetsClient.createProjectTargetWorkDTOLink(projectLink);
  }

  createLinkProjectOffer(projectId: number, tribeOfferId: number){
    let projectLink = new ProjectTribeOffers({projectId, tribeOfferId});
    return this.offersClient.createProjectTribeOfferLink(projectLink);
  }

  downloadRef(projectId: number){
    return this.http.get(this.baseUrl + 'projects/download/' + projectId, {
        observe: 'response',
        responseType: 'blob',
      });
  }
  
}
