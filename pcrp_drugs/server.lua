RegisterServerEvent('PCRP_Drugs:getSkunk')
AddEventHandler('PCRP_Drugs:getSkunk', function()
    local src = source
    local charInfo = exports["drp_id"]:GetCharacterData(src)
    TriggerEvent("DRP_Inventory:CheckWeight", charInfo, PCRPSkunk.Item, 1, function(weightCheck)
        if weightCheck == true then
            TriggerEvent("DRP_Inventory:addInventoryItem", PCRPSkunk.Item, PCRPSkunk.Amount)
            TriggerEvent("PCRP_Drugs:getSkunk", src)
            TriggerClientEvent("DRP_Core:Success", src, "Inventory", tostring("You got 1 Skunk!"), 2500, false, "leftCenter")
        else
            TriggerClientEvent("DRP_Core:Error", src, "Inventory", tostring("You don't have enough room in your inventory!"), 2500, false, "leftCenter")
        end
    end)
end)