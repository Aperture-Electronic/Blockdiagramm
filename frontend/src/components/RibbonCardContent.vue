<template>
  <div class="row">
    <template v-for="(column, index) in columns" :key="index">
      <template v-if="column.type == 'big-button'">
        <div class="col-md-auto q-px-none flex" style="align-items: center">
          <RibbonBigButton
            :title="column.title"
            :icon="column.icon"
            :menu="column.menu"
            :tooltip="column.tooltip"
          />
        </div>
      </template>
      <template v-else-if="column.type == 'list-3rows'">
        <div class="col-md-auto column q-px-none flex">
          <template v-for="(item, index) in column.items" :key="index">
            <RibbonSmallButton
              v-if="item.type == 'small-button'"
              :title="item.title"
              :icon="item.icon"
              :menu="item.menu"
              :tooltip="item.tooltip"
              :action="item.action"
            />
            <template v-else-if="item.type == 'selective'">
              <RibbonSelective
                :title="item.title"
                :icon="item.icon"
                :selection="item.selection"
                :tooltip="item.tooltip"
              />
            </template>
            <template v-else-if="item.type == 'toggle'">
              <RibbonToggle
                :title="item.title"
                :icon="item.icon"
                :tooltip="item.tooltip"
              />
            </template>
          </template>
        </div>
      </template>
    </template>
  </div>
</template>

<script lang="ts">
import RibbonBigButton from './RibbonCards/common/RibbonBigButton.vue';
import RibbonSmallButton from './RibbonCards/common/RibbonSmallButton.vue';
import RibbonSelective from './RibbonCards/common/RibbonSelective.vue';
import RibbonToggle from './RibbonCards/common/RibbonToggle.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'RibbonCardContent',
  components: {
    RibbonBigButton,
    RibbonSmallButton,
    RibbonSelective,
    RibbonToggle,
  },
  props: ['columns'],
  setup() {
    return {};
  },
});
</script>
