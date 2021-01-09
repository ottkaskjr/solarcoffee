import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import moment from 'moment';

Vue.config.productionTip = false;

// using in Inventory.vue
Vue.filter('price', function(number: number) {
  if (isNaN(number)) {
    return '-';
  }
  return '$' + number.toFixed(2);
});
// using in Customer.vue
Vue.filter('humanizeDate', function(date: Date) {
  return moment(date).format('MMMM Do YYYY');
});

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
