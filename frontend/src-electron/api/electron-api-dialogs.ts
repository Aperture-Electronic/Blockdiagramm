export async function openFolderDialog() {
  const { dialog } = require('electron');
  const result = dialog.showOpenDialogSync({
    properties: ['openDirectory'],
  });
  return result;
}

export async function openMultiSourceFileDialog() {
  const { dialog } = require('electron');
  const result = dialog.showOpenDialogSync({
    properties: ['openFile', 'multiSelections'],
    filters: [
      {
        name: 'Design Files',
        extensions: ['v', 'sv', 'svh', 'vhd', 'vhdl'],
      },
      {
        name: 'System Verilog & Verilog',
        extensions: ['v', 'sv', 'svh'],
      },
      { name: 'VHDL', extensions: ['vhd', 'vhdl'] },
      { name: 'Verilog', extensions: ['v'] },
      { name: 'All Files', extensions: ['*'] },
    ],
  });
  return result;
}
