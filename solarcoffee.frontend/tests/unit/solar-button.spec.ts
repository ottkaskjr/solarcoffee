import { mount, shallowMount } from '@vue/test-utils';
import SolarButton from '@/components/SolarButton.vue';

describe('SolarButton.vue', () => {
  it('displays text in default slot position', () => {
    const wrapper = shallowMount(SolarButton, {
      propsData: {},
      slots: {
        default: 'click here!',
      },
    });
    expect(wrapper.find('button').text()).toBe('click here!');
  });

  // check disabled button
  it('has underlying disabled button when disabled true passed as prop', () => {
    const wrapper = shallowMount(SolarButton, {
      propsData: {
        disabled: true,
      },
      slots: {
        default: 'foo',
      },
    });
    expect(wrapper.find('input:disabled'));
  });

  // inverse for disabled button
  it('has no disabled button when disabled false passed as prop', () => {
    const wrapper = shallowMount(SolarButton, {
      propsData: {
        disabled: false,
      },
      slots: {
        default: 'foo',
      },
    });
    expect(!wrapper.find('input:disabled'));
  });
});
