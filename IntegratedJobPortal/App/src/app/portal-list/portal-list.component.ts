import { Component, OnInit } from '@angular/core';
import { ApiResponse, CustomStatusCode } from 'src/models/ApiResponse';
import { Portal, PortalEntity } from 'src/models/Portal';
import { HttpCommonService } from 'src/services/http-common.service';
import { PortalService } from 'src/services/portal.service';

@Component({
  selector: 'app-portal-list',
  templateUrl: './portal-list.component.html',
  styleUrls: ['./portal-list.component.css']
})
export class PortalListComponent implements OnInit {

  constructor(private httpService: PortalService) { }

  portalList: Portal[]

  selectedPortalId: number = 0
  selectedPortalName: string = ''
  addPortalName: string = ''
  employeeId = 2619

  message: string = '';




  ngOnInit(): void {
    this.loadPortals();

  }

  loadPortals() {
    this.httpService.getPortalsList().subscribe((response: ApiResponse) => {
      if (response.StatusCode == CustomStatusCode.Success) {
        this.portalList = response.Data
        console.log(this.portalList)
      }
    });
  }

  select(portal: Portal) {
    console.log(portal)
    this.selectedPortalId = portal.PortalId;
    this.selectedPortalName = portal.PortalName;

  }

  add() {
    console.log(this.addPortalName)
    var portal = new PortalEntity(0, this.addPortalName, this.employeeId, 0, '', '', '');
    this.httpService.addPortal(portal).subscribe((response: ApiResponse) => {
      if (response.StatusCode == CustomStatusCode.Success) {
        this.addPortalName = ''
        this.message = 'Portal Added.';
        setTimeout(() => {
          this.message = '';
        }, 3000);
        this.loadPortals();
      }
    });
  }

  update() {
    var portal = new PortalEntity(this.selectedPortalId, this.selectedPortalName, this.employeeId, 0, '', '', '');
    this.httpService.updatePortal(portal).subscribe((response: ApiResponse) => {
      if (response.StatusCode == CustomStatusCode.Success) {
        this.selectedPortalId = 0
        this.selectedPortalName = ''
        this.message = 'Portal Updated.';
        setTimeout(() => {
          this.message = '';
        }, 3000);
        this.loadPortals();

      }
    });
  }

}
