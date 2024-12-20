import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  getNotes,
  createNote,
  updateNote,
  deleteNote,
} from "../api/houseApi.tsx";
import LoadingSpinner from "./LoadingSpinner";
import NoteCard from "./NoteCard.tsx";
import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { CreateNote } from "../modules/types";
import { useAuth } from "./AuthProvider";
import ConfirmationModal from "./ConfirmationModal";

const noteSchema = z.object({
  note: z
    .string()
    .min(2, "note must be at least 2 characters long")
    .max(200, "note cannot be more than 200 characters long"),
});

type Props = { houseId: string; roomId: string };

const noteSection = ({ houseId, roomId }: Props) => {
  const [editingIndex, setEditingIndex] = useState<null | number>(null);
  const [editedNote, setEditedNote] = useState("");
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [noteToDelete, setNoteToDelete] = useState<string | null>(null);
  const { currentUser } = useAuth();

  const queryClient = useQueryClient();

  const { data: notes, isLoading } = useQuery({
    queryKey: ["notes", { houseId, roomId }],
    queryFn: () => getNotes(houseId, roomId),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm({
    resolver: zodResolver(noteSchema),
  });

  const mutationcreateNote = useMutation({
    mutationFn: (noteText: CreateNote) =>
      createNote(houseId, roomId, noteText),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["notes", { houseId, roomId }],
      });
      reset();
      toast.success("note added successfully!");
    },
    onError: () => {
      toast.error("Error creating note.");
    },
  });

  const mutationupdateNote = useMutation({
    mutationFn: (updatedNote: { noteId: string; note: string }) =>
      updateNote(houseId, roomId, updatedNote.noteId, {
        note: updatedNote.note,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["notes", { houseId, roomId }],
      });
      setEditingIndex(null);
      toast.success("note updated successfully!");
    },
    onError: () => {
      toast.error("Error updating note.");
    },
  });

  const mutationdeleteNote = useMutation({
    mutationFn: (noteId: string) =>
      deleteNote(houseId, roomId, noteId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["notes", { houseId, roomId }],
      });
      toast.success("note deleted successfully!");
    },
    onError: () => {
      toast.error("Error deleting note.");
    },
  });

  const handleEdit = (index: number) => {
    setEditingIndex(index);
    setEditedNote(notes![index].note);
  };

  const handleSave = async () => {
    if (editedNote.length < 2) {
      toast.error("note must be at least 2 characters long.");
      return;
    }

    const noteId = notes![editingIndex!].id;

    mutationupdateNote.mutate({ noteId, note: editedNote });

    setEditedNote("");
  };

  const handleDelete = (noteId: string) => {
    setNoteToDelete(noteId);
    setIsDeleteModalOpen(true);
  };

  const confirmDelete = () => {
    if (noteToDelete) {
      mutationdeleteNote.mutate(noteToDelete);
      setIsDeleteModalOpen(false);
      setNoteToDelete(null);
    }
  };

  const cancelDelete = () => {
    setIsDeleteModalOpen(false);
    setNoteToDelete(null);
  };

  const onSubmit = (data: { note: string }) => {
    mutationcreateNote.mutate({ note: data.note });
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <LoadingSpinner />
      </div>
    );
  }

  return (
    <div>
          <hr className="my-8 border-t-6 border-purple-600" />
      <h2 className="text-2xl font-semibold text-center mb-6">notes</h2>

      {currentUser && (
        <div className="bg-white p-4 rounded-lg shadow-md mb-4">
          <form onSubmit={handleSubmit(onSubmit)} noValidate>
            <textarea
              {...register("note")}
              className="w-full p-2 border border-gray-300 rounded mb-4 min-h-20"
              placeholder="Add a new note..."
            />
            {errors.note && (
              <p className="text-red-500 text-sm">{errors.note.message}</p>
            )}
            <div className="flex justify-end">
              <button
                type="submit"
                className="py-3 px-6 mr-5 bg-purple-500 text-white rounded-lg hover:bg-purple-600 transition-all duration-300"
              >
                Add note
              </button>
            </div>
          </form>
        </div>
      )}

      {editingIndex !== null ? (
        <div className="bg-white p-4 rounded-lg shadow-md mb-4">
          <textarea
            value={editedNote}
            onChange={(e) => setEditedNote(e.target.value)}
            className="w-full p-2 border border-gray-300 rounded mb-4"
          />
          <div className="flex justify-end">
            <button
              onClick={handleSave}
              className="py-3 px-6 mr-5 bg-purple-500 text-white rounded-lg hover:bg-purple-600 transition-all duration-300"
            >
              Save
            </button>
            <button
              onClick={() => setEditingIndex(null)}
              className="text-purple-600 hover:text-purple-800"
            >
              Cancel
            </button>
          </div>
        </div>
      ) : notes === null || notes?.length === 0 ? (
        <div className="text-center text-xl text-gray-400">
          <p>No notes Found</p>
        </div>
      ) : (
        <div className="p-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {notes?.map((note, index) => {
            return (
              <NoteCard
                key={note.id}
                note={note}
                index={index}
                onEdit={handleEdit}
                onDelete={handleDelete}
              />
            );
          })}
        </div>
      )}

      <ConfirmationModal
        isOpen={isDeleteModalOpen}
        onClose={cancelDelete}
        onConfirm={confirmDelete}
        message="Are you sure you want to delete this note? This action cannot be undone."
      />
    </div>
  );
};

export default noteSection;
