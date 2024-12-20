import { Link, useNavigate, useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { deleteHouse, getHouse } from "../api/houseApi.tsx";
import NotFoundPage from "./NotFoundPage";
import LoadingSpinner from "../components/LoadingSpinner";
import HouseForm from "../components/HouseForm.tsx";
import { useState } from "react";
import RoomSection from "../components/RoomSection";
import { useAuth } from "../components/AuthProvider";
import { toast } from "react-toastify";
import {
    FaMapMarkerAlt,
} from "react-icons/fa";
import ConfirmationModal from "../components/ConfirmationModal"; // Import the modal

const HousePage = () => {
    const { houseId } = useParams();
    const [isEditMode, setIsEditMode] = useState(false);
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false); // State for modal visibility
    const { currentUser } = useAuth();
    const navigate = useNavigate();

    if (!houseId || isNaN(parseInt(houseId))) {
        return <NotFoundPage />;
    }

    const { data: house, isLoading } = useQuery({
        queryKey: ["house", { houseId }],
        queryFn: () => getHouse(houseId),
    });

    if (isLoading) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <LoadingSpinner />
            </div>
        );
    }

    if (!house) {
        return <NotFoundPage />;
    }

    const isAdmin = currentUser?.groups.includes("Admin");

    // Handle house deletion
    const handleDelete = async () => {
        try {
            const response = await deleteHouse(houseId);
            if (response === "success") {
                toast.success("House Deleted Successfully");
                navigate("/");
            } else {
                toast.error("House Deletion Failed");
            }
        } catch (error) {
            toast.error("An error occurred while deleting the house.");
        }
    };

    // Open and close the confirmation modal
    const openDeleteModal = () => {
        setIsDeleteModalOpen(true);
    };

    const closeDeleteModal = () => {
        setIsDeleteModalOpen(false);
    };

    return (
        <div className="container mx-auto py-12 px-4 sm:px-6 lg:px-8">
            <div className="flex flex-col">
                {isAdmin && !isEditMode && (
                    <div className="flex justify-end mt-4 space-x-4">
                        <button
                            className="px-6 py-2 bg-purple-600 text-white rounded-lg hover:bg-purple-700"
                            onClick={() => setIsEditMode(true)}
                        >
                            Edit
                        </button>
                        <button
                            onClick={openDeleteModal}
                            className="px-6 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
                        >
                            Delete
                        </button>
                    </div>
                )}
            </div>

            {isEditMode ? (
                <HouseForm house={house} onClose={() => setIsEditMode(false)} />
            ) : (
                <div className="mt-10">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                        <div className="space-y-4">
                            <h2 className="text-3xl sm:text-4xl font-bold text-purple-600 break-words">
                                {house?.name}
                            </h2>
                            <p className="text-base sm:text-lg text-gray-600 break-words">
                                {house?.region}
                            </p>

                            <div className="text-base sm:text-lg text-gray-600">
                                <p className="flex items-center mb-2">
                                    <FaMapMarkerAlt className="mr-2 text-purple-600" />
                                    <span className="font-semibold mr-2">District: </span>
                                    {house?.district}
                                </p>
                            </div>
                        </div>
                    </div>

                    {isAdmin && (
                        <div className="flex justify-end mt-4 relative">
                            <Link
                                to={`/house/${houseId}/room`}
                                className="absolute top-10 right-4 bg-green-600 text-white p-4 rounded-full shadow-lg hover:bg-green-700 transition duration-300"
                            >
                                <span className="text-2xl">+</span>
                            </Link>
                        </div>
                    )}

                    <RoomSection houseId={houseId} />
                </div>
            )}

            <ConfirmationModal
                isOpen={isDeleteModalOpen}
                onClose={closeDeleteModal}
                onConfirm={handleDelete}
                message="Are you sure you want to delete this house? This action cannot be undone."
            />
        </div>
    );
};

export default HousePage;
