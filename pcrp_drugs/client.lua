local sleeper = 0
local searchedObjects = {}
local pickingSkunk = false
Citizen.CreateThread(function()
    Citizen.Wait(100)
    while true do
        local sleeper = 1000
        local ped = PlayerPedId()
        local playerCoords = GetEntityCoords(ped, false)
        for a = 1, #PCRPSkunk.Skunk do
            local skunkPlant = GetClosestObjectOfType(playerCoords, 1.0, GetHashKey(PCRPSkunk.Skunk[a]), false, false, false)
            local object = nil
            if DoesEntityExist(skunkPlant) then
                sleeper = 5
                DisplayHelpText("Press E For "..PCRPSkunk.Item.." $"..PCRPSkunk.Amount)
                if IsControlJustPressed(1, 38) then
                    print("You got it mate! - Well done being a criminal! ;)")
                    TriggerServerEvent("PCRP_Drugs:getSkunk")
                end
                break
            else
                sleeper = 1000
            end
        end
        Citizen.Wait(sleeper)
    end
end)

RegisterNetEvent('PCRP_Drugs:getSkunk')
AddEventHandler('PCRP_Drugs:getSkunk', function()
    pickingSkunk = true
    RequestAnimDict('amb@medic@standing@kneel@base')  
    while not HasAnimDictLoaded('amb@medic@standing@kneel@base') do
        Citizen.Wait(0)
    end
    TaskPlayAnim(PlayerPedId(), 'amb@medic@standing@kneel@base', 'base', 0.7, 0.7, 2000, 0, 1, true, true, true)
    DisplayHelpText("Picking "..PCRPSkunk.Amount.." $"..PCRPSkunk.Item)
    Citizen.Wait(3000)
    TriggerServerEvent("DRP_Inventory:addInventoryItem", PCRPSkunk.Item, tonumber(1))
    ClearPedTasks(PlayerPedId())
    pickingSkunk = false

end)

function DisplayHelpText(str)
	SetTextComponentFormat("STRING")
	AddTextComponentString(str)
	DisplayHelpTextFromStringLabel(0, 0, 1, -1)
end