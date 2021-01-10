import { /*mount,*/ shallowMount } from '@vue/test-utils';
// with 'mount' we can do "deepMount" instead of "shallowMount" which includes all the dependent components
import HelloWorld from '@/components/HelloWorld.vue';

describe('HelloWorld.vue', () => {
  it('renders props.msg when passed', () => {
    // Arrange
    const msg = 'new message';

    // Act
    const wrapper = shallowMount(HelloWorld, {
      propsData: { msg },
    });

    // Assert
    expect(wrapper.text()).toMatch(msg);
  });
});
