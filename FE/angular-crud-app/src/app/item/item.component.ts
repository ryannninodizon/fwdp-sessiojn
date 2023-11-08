import { Component, OnInit } from '@angular/core';
import { ItemService } from '../item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  items: any[] = [];
  selectedItem: any = {};

  constructor(private itemService: ItemService) {}

  ngOnInit() {
    this.itemService.getItems().subscribe(items => {
      this.items = items;
    });
  }

  createItem(item: any) {
    this.itemService.createItem(item).subscribe(() => {
      this.items.push(item);
    });
  }

  updateItem(item: any) {
    this.itemService.updateItem(item).subscribe(() => {
      this.selectedItem = {};
    });
  }

  deleteItem(id: number) {
    this.itemService.deleteItem(id).subscribe(() => {
      this.items = this.items.filter(item => item.id !== id);
    });
  }

  onSelect(item: any) {
    this.selectedItem = { ...item };
  }

  clearSelection() {
    this.selectedItem = {};
  }
}