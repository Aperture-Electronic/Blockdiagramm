<template>
  <q-menu class="ribbon-menu" auto-close :offset="[0, 5]">
    <q-list>
      <template v-for="(item, index) in menu" :key="index">
        <q-item
          v-if="item.type == 'menu-button-item'"
          clickable
          class="q-px-sm"
        >
          <q-item-section>
            <div class="row flex">
              <div v-if="item.icon == ''" style="width: 1.5em"></div>
              <q-icon
                v-else
                :name="item.icon"
                size="1.5em"
                class="col-auto q-ml-none"
              />
              <div
                class="col-auto q-ml-sm"
                style="align-self: center; min-width: 85px"
                v-html="item.title"
              ></div>
              <div
                class="text-overline col-auto q-ml-md"
                style="align-self: center"
                v-if="item.shortcut != ''"
                v-html="item.shortcut"
              ></div>
            </div>
            <RibbonToolTipBox :tooltip="item.tooltip" :fixed="false" />
          </q-item-section>
        </q-item>
        <q-separator v-else-if="item.type == 'menu-separator'" />
      </template>
    </q-list>
  </q-menu>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import RibbonToolTipBox from './RibbonToolTipBox.vue';

export default defineComponent({
  name: 'RibbonBigButton',
  components: { RibbonToolTipBox },
  props: ['menu'],
  setup() {
    return {};
  },
});
</script>
