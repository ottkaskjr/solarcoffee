import { ICustomer } from '@/types/Customer';
import { IServiceRespone } from '@/types/ServiceResponse';
import axios from 'axios';

/**
 * Customer Service
 * Provides UI business logic associated with Customers in our system
 */
export default class CUstomerService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getCustomers(): Promise<ICustomer[]> {
    const result: any = await axios.get(`${this.API_URL}/customer/`);
    return result.data;
  }

  public async addCustomer(
    newCustomer: ICustomer
  ): Promise<IServiceRespone<ICustomer>> {
    const result: any = await axios.post(
      `${this.API_URL}/customer/`,
      newCustomer
    );
    return result.data;
  }

  public async deleteCustomer(customerId: number): Promise<boolean> {
    const result: any = await axios.delete(
      `${this.API_URL}/customer/${customerId}`
    );
    return result.data;
  }
}
