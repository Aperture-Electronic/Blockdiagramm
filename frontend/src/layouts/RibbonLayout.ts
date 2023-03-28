import ProjectCard from 'src/layouts/Ribbon/ProjectCard';
import SourceCard from 'src/layouts/Ribbon/SourceCard';
import BitLogicCard from './Ribbon/BitLogicCard';
import BitOperationCard from './Ribbon/BitOperationCard';
import BuildCard from './Ribbon/BuildCard';
import BusCard from './Ribbon/BusCard';
import ComponentCard from './Ribbon/ComponentCard';
import DiagramCard from './Ribbon/DiagramCard';
import EditCard from './Ribbon/EditCard';
import ElaborateCard from './Ribbon/ElaborateCard';
import GenerateCard from './Ribbon/GenerateCard';
import GirdCard from './Ribbon/GirdCard';
import HelpCard from './Ribbon/HelpCard';
import PipelineCard from './Ribbon/PipelineCard';
import PortCard from './Ribbon/PortCard';
import SearchCard from './Ribbon/SearchCard';
import UndoCard from './Ribbon/UndoCard';
import WireCard from './Ribbon/WireCard';
import ZoomCard from './Ribbon/ZoomCard';

const ribbon = {
  panels: [
    {
      name: 'file',
      default: true,
      title: 'File',
      cards: [
        {
          name: 'project',
          title: 'Project',
          content: ProjectCard,
        },
        {
          name: 'source',
          title: 'Source',
          content: SourceCard,
        },
        {
          name: 'elaborate',
          title: 'Elaborate',
          content: ElaborateCard,
        },
        {
          name: 'build',
          title: 'Build',
          content: BuildCard,
        },
      ],
    },
    {
      name: 'design',
      default: false,
      title: 'Design',
      cards: [
        {
          name: 'diagram',
          title: 'Diagram',
          content: DiagramCard,
        },
        {
          name: 'undo',
          title: 'Undo',
          content: UndoCard,
        },
        {
          name: 'edit',
          title: 'Edit',
          content: EditCard,
        },
        {
          name: 'component',
          title: 'Component',
          content: ComponentCard,
        },
        {
          name: 'wire',
          title: 'Wire',
          content: WireCard,
        },
        {
          name: 'port',
          title: 'Port',
          content: PortCard,
        },
        {
          name: 'search',
          title: 'Search',
          content: SearchCard,
        },
      ],
    },
    {
      name: 'insert',
      default: false,
      title: 'Insert',
      cards: [
        {
          name: 'bit-operation',
          title: 'Bit Operation',
          content: BitOperationCard,
        },
        {
          name: 'bit-logic',
          title: 'Bit Logic',
          content: BitLogicCard,
        },
        {
          name: 'bus',
          title: 'Bus',
          content: BusCard,
        },
        {
          name: 'pipeline',
          title: 'Pipeline',
          content: PipelineCard,
        },
        {
          name: 'generate',
          title: 'Generate',
          content: GenerateCard,
        },
      ],
    },
    {
      name: 'view',
      default: false,
      title: 'View',
      cards: [
        {
          name: 'zoom',
          title: 'Zoom',
          content: ZoomCard,
        },
        {
          name: 'grid',
          title: 'Grid',
          content: GirdCard,
        },
      ],
    },
    {
      name: 'help',
      default: false,
      title: 'Help',
      cards: [
        {
          name: 'help',
          title: 'Help',
          content: HelpCard,
        },
      ],
    },
  ],
};

export default ribbon;
