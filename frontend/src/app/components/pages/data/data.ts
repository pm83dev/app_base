import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngxs/store';
import { DataItem } from '../../../interface/data.interface';
import { DataState, LoadApiData } from '../../../state/data.state';
import { Modal } from '../../common/modal/modal';

@Component({
  selector: 'app-data',
  imports: [CommonModule, Modal, FormsModule],
  templateUrl: './data.html',
  styleUrls: ['./data.scss'],
})
export class Data {
  // Dependency injection for the NGXS store to access state and dispatch actions
  private readonly store = inject(Store);
  showModal = false;
  selectedItem: DataItem | null = null; // ← tiene l'item selezionato
  modalMode: 'add' | 'edit' = 'add';
  newItem: DataItem = { id: 0, name: '', value: '' }; // ← modello per il nuovo item

  public dataItems$ = this.store.select(DataState.data);

  constructor() {
    // Dispatch the action to load API data when the component is initialized
    this.store.dispatch(new LoadApiData());
  }

  editItem(item: DataItem) {
    this.selectedItem = item;
    this.modalMode = 'edit';
    this.showModal = true;
  }

  deleteItem(item: DataItem) {
    this.selectedItem = item;
    this.showModal = true;
  }

  addItem() {
    this.selectedItem = null;
    this.modalMode = 'add';
    this.showModal = true;
  }

  saveItem() {
    if (this.modalMode === 'add') {
      // dispatch azione di creazione
    } else {
      // dispatch azione di aggiornamento
    }
    this.showModal = false;
  }

  closeModal() {
    this.showModal = false;
  }
}
