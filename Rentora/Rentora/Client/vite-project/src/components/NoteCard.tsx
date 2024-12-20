import { Note } from "../modules/types";
import { useAuth } from "./AuthProvider";

type Props = {
  note: Note;
  onEdit: (index: number) => void;
  onDelete: (index: string) => void;
  index: number;
};

const NoteCard = ({ note, onEdit, onDelete, index }: Props) => {
  //const [date, longTime] = note.createdAt.toLocaleString().split("T");
  //const [time] = longTime.split(".");
  const { currentUser } = useAuth();

  ondevicemotion;

  return (
    <div className="bg-purple-100 p-4 mb-4 rounded-lg shadow-md border border-purple-300">
      <div className="flex justify-between items-center mb-2">
        <h3 className="text-lg font-semibold text-purple-800">
          {note.userName}
        </h3>
      </div>
      <p className="text-base text-gray-800 w-full overflow-hidden whitespace-nowrap text-ellipsis">
        {note.note}
      </p>
      {currentUser && (currentUser.userName === note.userName || currentUser.groups.includes("Admin")) && (
        <div className="flex justify-end mt-2">
          <button
            onClick={() => onEdit(index)}
            className="bg-purple-500 text-white px-3 py-1 rounded mr-2 hover:bg-purple-400"
          >
            Edit
          </button>
          <button
            onClick={() => onDelete(note.id)}
            className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-400"
          >
            Delete
          </button>
        </div>
      )}
    </div>
  );
};

export default NoteCard;
