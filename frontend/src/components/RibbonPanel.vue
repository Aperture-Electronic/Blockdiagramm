<template>
  <q-card flat class="ribbon-card">
    <q-tabs
      v-model="tab"
      dense
      class="text-grey"
      active-color="primary"
      indicator-color="primary"
      align="left"
    >
      <q-tab
        v-for="panel in ribbon.panels"
        :key="panel.name"
        :name="panel.name"
        :label="panel.title"
        class="ribbon-title"
      />
    </q-tabs>
    <q-separator />
    <q-tab-panels v-model="tab" animated class="text-black">
      <q-tab-panel
        v-for="panel in ribbon.panels"
        :key="panel.name"
        :name="panel.name"
        class="q-py-none q-px-none"
      >
        <q-card flat>
          <q-card-section horizontal>
            <template v-for="(card, index) in panel.cards" :key="card.name">
              <RibbonCard :title="card.title" :cardContent="card.content" />
              <q-separator
                v-if="index != panel.cards.length - 1"
                vertical
                inset
              />
            </template>
            <RibbonCard v-if="panel.cards.length == 0" title="Empty" />
          </q-card-section>
        </q-card>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { ref } from 'vue';
import RibbonCard from 'src/components/RibbonCard.vue';

export default defineComponent({
  name: 'RibbonPanel',
  components: { RibbonCard },
  props: ['ribbon'],
  setup(props) {
    let defRibbonPanel = props.ribbon.panels.find(
      (p: { default: object }) => p.default
    );
    if (defRibbonPanel == undefined) {
      defRibbonPanel = ref(props.ribbon.panels[0]);
    }

    return {
      tab: ref(defRibbonPanel.name),
    };
  },
});
</script>
