windows = _GetWindows(false, true)
baseGravity = 0.53
--gravityMultiplier = 0.5
yVelocity = 0
yMaxVelocity = 30
jump = false
grounded = false

function _Start()
    pet.animation = "idle_4"
    pet.x = 720
    pet.y = 0
end

function _Update()
    -- take input
    jump = _IsKeyHeld("Space")

    if _IsKeyHeld("D") then
        pet.animation = "run_6"
        pet.flipX = false
        pet.x = pet.x + 8
    elseif _IsKeyHeld("A") then
        pet.animation = "run_6"
        pet.flipX = true
        pet.x = pet.x - 8
    else
        pet.animation = "idle_4"
    end

    if jump or not grounded then
        pet.animation = "jump_1"

        if yVelocity > 3.5 then
            pet.animation = "fall_1"
        end
    end
end

function _LateUpdate()
    windows = _GetWindows(false, true)

    yVelocity = yVelocity + baseGravity
    grounded = false

    if (yVelocity > yMaxVelocity) then 
        yVelocity = yMaxVelocity
    end

    -- check window collisions
    for i = 0, tablelength(windows) - 1, 1 do
        -- adjusted pet bounds
        ab = BoundsFromXYWH(pet.bounds.X + 52, pet.y - 20, pet.bounds.Width - 128, 20)
        -- projected pet bounds
        pb = BoundsFromXYWH(ab.X, ab.Y, ab.Width, ab.Height)
        pb.Height = ab.Height + yVelocity

        wb = windows[i].bounds
        wb = BoundsFromXYWH(wb.X, wb.Y, wb.Width, 20)
        
        -- if i == 0 then
        --     print(windows[i].name)
        --     print(ab.X .. " " .. ab.Y .. " " .. ab.Width .. " " .. ab.Height)
        --     print(wb.X .. " " .. wb.Y .. " " .. wb.Width .. " " .. wb.Height)
        -- end

        -- check projected and current bounds against the window bounds
        if (isColliding(pb, wb) or isColliding(ab, wb)) and yVelocity >= 0 then
            pet.y = wb.Y
            yVelocity = 0
            grounded = true
        end
    end
    
    -- check ground collision
    if pet.y >= desktopBounds.Height then
        pet.y = desktopBounds.Height
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
