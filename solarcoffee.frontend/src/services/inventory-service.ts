import axios from 'axios';
import { IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';

/*
 * Inventory Service.
 * Provides UI business logic associated with product inventory
 */
export class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;
  // IF WE DON'T KNOW THE TYPE WE CAN USE 'any'(Promise<any>)
  public async getInventory(): Promise<IProductInventory> {
    const result = await axios.get(`${this.API_URL}/inventory/`);
    return result.data;
  }

  public async updateInventoryQuantity(shipment: IShipment) {
    const result = await axios.patch(`${this.API_URL}/inventory/`, shipment);
    return result.data;
  }

  public async getSnapshotHistory(): Promise<IInventoryTimeline> {
    const result: any = await axios.get(`${this.API_URL}/inventory/snapshot`);
    return result.data;
  }
}
