<template>
  <q-item :active="selected" dense clickable v-ripple @click="onItemClick">
    <div class="flex justify-center flex-center">
      <q-icon class="q-pr-xs" size="1.25em" name="mdi-file"></q-icon>
    </div>
    <q-item-section style="overflow: hidden">
      <q-item-label lines="1">
        {{ sourceFile.ShortName }}
      </q-item-label>
    </q-item-section>
    <q-item-section side>
      <q-chip square dense :color="sourceFileChip.color" text-color="white">
        {{ sourceFileChip.abbr }}
      </q-chip>
    </q-item-section>
    <SourceItemToolTip :file="file" />
  </q-item>
</template>

<script lang="ts">
import SourceFile from 'src/tools/server/sources/SourceFile';
import { SourceType } from 'src/tools/server/sources/SourceFileType';
import { defineComponent } from 'vue';
import SourceItemToolTip from './SourceItemToolTip.vue';

function getSourceFileChip(sourceFile: SourceFile) {
  if (!sourceFile.Exist) {
    return { abbr: 'Invalid', color: 'red' };
  } else {
    // Return the file type abbr. and the color at same time
    switch (sourceFile.Type) {
      case SourceType.SystemVerilog:
        return { abbr: 'SV', color: 'blue' };
      case SourceType.Verilog:
        return { abbr: 'V', color: 'green' };
      case SourceType.VHDL:
        return { abbr: 'VHDL', color: 'orange' };
      default:
        return { abbr: 'Unknown', color: 'grey' };
    }
  }
}

export default defineComponent({
  name: 'SourceItem',
  props: ['file', 'selected'],
  components: {
    SourceItemToolTip,
  },
  data() {
    let sourceFile: SourceFile = this.file;
    let sourceFileChip = getSourceFileChip(sourceFile);

    return {
      sourceFile,
      sourceFileChip,
    };
  },
  watch: {
    file: {
      handler: function (newVal: SourceFile) {
        this.sourceFile = newVal;
        this.sourceFileChip = getSourceFileChip(newVal);
      },
    },
  },
  setup() {
    return {};
  },
  methods: {
    onItemClick() {
      this.$emit('click');
    },
  },
});
</script>
