<template>
  <q-tooltip
    anchor="bottom start"
    self="top left"
    transition-show="scale"
    transition-hide="scale"
    class="source-item-tooltip"
  >
    <q-card flat class="q-px-none q-py-none">
      <q-card-section class="q-py-xs">
        <div class="text-caption text-weight-bold">
          {{ sourceFile.ShortName }}
        </div>
      </q-card-section>
      <q-separator class="q-mx-sm" />
      <q-card-section class="q-py-sm">
        <div class="text-body2 text-grey-9">
          {{ getSourceTypeDescription(sourceFile.Type) }}
        </div>
      </q-card-section>
      <q-card-section class="q-py-sm">
        <div>{{ sourceFile.Path }}</div>
      </q-card-section>
    </q-card>
  </q-tooltip>
</template>

<script lang="ts">
import SourceFile from 'src/tools/server/sources/SourceFile';
import { SourceType } from 'src/tools/server/sources/SourceFileType';
import { defineComponent } from 'vue';

function getSourceTypeDescription(type: SourceType) {
  switch (type) {
    case SourceType.SystemVerilog:
      return 'System Verilog Source File';
    case SourceType.Verilog:
      return 'Verilog Source File';
    case SourceType.VHDL:
      return 'VHDL Source File';
    case SourceType.SystemVerilogHeader:
      return 'System Verilog Header File';
    default:
      return 'Unknown File';
  }
}

export default defineComponent({
  name: 'SourceItemToolTip',
  components: {},
  data() {
    let sourceFile: SourceFile = this.file;
    return {
      sourceFile,
    };
  },
  props: ['file'],
  watch: {
    file: {
      handler: function (newVal: SourceFile) {
        this.sourceFile = newVal;
      },
    },
  },
  setup() {
    return {};
  },
  methods: {
    getSourceTypeDescription,
  },
});
</script>

<style>
.source-item-tooltip {
  background-color: white;
  color: black;
  box-shadow: 0 5px 5px 0 rgba(0, 0, 0, 0.25);
  border-radius: 4px;
  padding: 0;
  outline: 2px solid rgba(0, 0, 0, 0.25);
}
</style>
