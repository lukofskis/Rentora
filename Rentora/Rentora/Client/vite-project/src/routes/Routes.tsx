import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../pages/HomePage";
import NotFoundPage from "../pages/NotFoundPage";
import HousePage from "../pages/HousePage.tsx";
import RoomPage from "../pages/RoomPage";
import LoginPage from "../pages/LoginPage";
import CreateHousePage from "../pages/CreateHousePage.tsx";
import RegisterPage from "../pages/RegisterPage";
import ProtectedRoute from "../components/ProtectedRoute";
import CreateRoomPage from "../pages/CreateRoomPage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "",
                element: <HomePage />,
            },
            {
                path: "register",
                element: <RegisterPage />,
            },
            {
                path: "login",
                element: <LoginPage />,
            },
            {
                path: "house/:houseId",
                element: <HousePage />,
            },
            {
                path: "house",
                element: (
                    <ProtectedRoute allowedRoles={["Admin"]}>
                        <CreateHousePage />
                    </ProtectedRoute>
                ),
            },
            {
                path: "house/:houseId/room/:roomId",
                element: <RoomPage />,
            },
            {
                path: "house/:houseId/room",
                element: (
                    <ProtectedRoute allowedRoles={["Admin"]}>
                        <CreateRoomPage />
                    </ProtectedRoute>
                ),
            },
            {
                path: "*",
                element: <NotFoundPage />,
            },
        ],
    },
]);