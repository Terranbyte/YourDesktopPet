windows = _GetWindows(false, true)
baseGravity = 0.53
desktopHeight = desktopBounds.h
--gravityMultiplier = 0.5
yVelocity = 0
yMaxVelocity = 30
jump = false
grounded = false

function _Start()
    pet.animation = "idle"
    pet.x = desktopBounds.w/2
    pet.y = desktopHeight
    show = true
end

function _Update()
    -- take input
    jump = _IsKeyDown(virtualKey.SPACE)
    
    if _IsKeyHeld(virtualKey.D) then
        pet.animation = "run"
        pet.flipX = false
        pet.x = pet.x + 8
    elseif _IsKeyHeld(virtualKey.A) then
        pet.animation = "run"
        pet.flipX = true
        pet.x = pet.x - 8
    else
        pet.animation = "idle"
    end
    
    -- print(jump)

    if jump or not grounded then
        pet.animation = "jump"
        
        if yVelocity > 3.5 then
            pet.animation = "fall"
        end
    end
end

function test()
    print("This is a test")
end

function _LateUpdate()
    windows = _GetWindows(false, true)
    -- windows = {}

    yVelocity = yVelocity + baseGravity
    grounded = false

    if (yVelocity > yMaxVelocity) then 
        yVelocity = yMaxVelocity
    end

    -- check window collisions
    for k, v in pairs(windows) do
        -- adjusted pet bounds
        --ab = BoundsFromXYWH(pet.bounds.x + 52, pet.y - 20, pet.bounds.w - 128, 20)
        -- projected pet bounds
        pb = BoundsFromXYWH(pet.bounds.x + 52, pet.y - 20, pet.bounds.w - 128, 20 + yVelocity)
        
        wb = BoundsFromXYWH(v.bounds.x, v.bounds.y, v.bounds.w, 20)
        -- if i == 0 then
        --     print(windows[i].name)
        --     print(ab.x .. " " .. ab.y .. " " .. ab.w .. " " .. ab.h)
        --     print(wb.x .. " " .. wb.y .. " " .. wb.w .. " " .. wb.h)
        -- end

        -- check projected and current bounds against the window bounds
        if IsColliding(pb, wb) and yVelocity >= 0 then
            pet.y = wb.y
            yVelocity = 0
            grounded = true
        end
    end
    
    -- check ground collision
    if pet.y >= desktopHeight then
        pet.y = desktopHeight
        yVelocity = 0
        grounded = true
    end

    if grounded and jump then
        yVelocity = -15
        grounded = false
    end

    pet.y = pet.y + yVelocity
end

function _Draw()
    --print("Draw")
end
