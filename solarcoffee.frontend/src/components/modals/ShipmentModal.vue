<!--suppress xmlUnboundNsPrefix -->
<template>
  <solar-modal>
    <template v-slot:header
      ><!--renders this in the slot with "name" attribute declared as "header"-->
      Receive Shipment
    </template>
    <template v-slot:body>
      <label for="product">Product Received:</label>

      <select v-model="selectedProduct" class="shipmentItems" id="product">
        <option disabled value="">Please select one</option>
        <option v-for="item in inventory" :value="item" :key="item.product.id">
          {{ item.product.name }}
        </option>
      </select>

      <label for="qtyReceived">Quantity Received:</label>
      <input type="number" id="qtyReceived" v-model="qtyReceived" />
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new shiptment"
      >
        Save Received Shipment
      </solar-button>
      <solar-button
        type="button"
        @button:click="close"
        aria-label="Close modal"
      >
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import SolarButton from '@/components/SolarButton';
import SolarModal from '@/components/modals/SolarModal';
import { IProduct, IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';

@Component({
  name: 'ShipmentModal',
  components: { SolarModal, SolarButton },
})
export default class ShipmentModal extends Vue {
  // importing invenvory as a prop
  @Prop({ required: true, type: Array as () => IProductInventory[] })
  inventory!: IProductInventory[];

  selectedProduct: IProduct = {
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    name: '',
    description: '',
    price: 0,
    isTaxable: false,
    isArchived: false,
  };

  qtyReceived = 0;

  close() {
    this.$emit('close');
  }

  save() {
    const shipment: IShipment = {
      productId: this.selectedProduct.id,
      adjustment: this.qtyReceived,
    };

    this.$emit('save:shipment', shipment);
  }
}
</script>

<style scoped lang="scss"></style>
