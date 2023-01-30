import { Injectable } from '@angular/core';
import { ProjectsClient } from '../web-api-client';

@Injectable({
  providedIn: 'root'
})
export class FinderService {

  constructor(private projectsClient: ProjectsClient) { }

  GetPaginatedProjects(pageId: number, pageSize: number, searchText: string, verticalId: string, sectorId: string,
    customerId: string, technologyId: string, offerId: string, targetId: string, countryId: string,
    isPrivateSelected: boolean, isPublicSelected: boolean) {
    return this.projectsClient.getProjects(pageId, pageSize, searchText, verticalId, sectorId, customerId, 
      technologyId, countryId, offerId, targetId, isPrivateSelected, isPublicSelected);
  }

}
